using System;
using System.Collections;
using System.Collections.Generic;
using RPGM.Gameplay;
using UnityEngine;
using UnityEngine.U2D;

namespace RPGM.Gameplay
{
    /// <summary>
    /// A simple controller for animating a 4 directional sprite using Physics.
    /// </summary>
    public class CharacterController2D : MonoBehaviour
    {
        public float speed = 1;
        public float acceleration = 2;
        public Vector3 nextMoveCommand;
        public Animator animator;
        public bool flipX = false;
        //added
        public FloatValue currentHealth;
        public Signal playerHealthSignal;
        public float knockTime;
        public Inventory playerInventory;
        public SpriteRenderer receivedItemSprite;
        public VectorValue startingPosition;
        //

        Rigidbody2D rigidbody2D;
        SpriteRenderer spriteRenderer;
      //  PixelPerfectCamera pixelPerfectCamera;

        enum State
        {
            Idle, Moving, Interact, Sitting
        }

        State state = State.Idle;
        Vector3 start, end;
        Vector2 currentVelocity;
        float startTime;
        float distance;
        float velocity;

        void IdleState()
        {
            if (nextMoveCommand != Vector3.zero)
            {
                //
                playerHealthSignal.Raise();
                //
                start = transform.position;
                end = start + nextMoveCommand;
                distance = (end - start).magnitude;
                velocity = 0;
                UpdateAnimator(nextMoveCommand);
                nextMoveCommand = Vector3.zero;
                state = State.Moving;
            }
        }

        void MoveState()
        {
            velocity = Mathf.Clamp01(velocity + Time.deltaTime * acceleration);
            UpdateAnimator(nextMoveCommand);
            rigidbody2D.velocity = Vector2.SmoothDamp(rigidbody2D.velocity, nextMoveCommand * speed, ref currentVelocity, acceleration, speed);
            spriteRenderer.flipX = rigidbody2D.velocity.x >= 0 ? true : false;
        }

        void UpdateAnimator(Vector3 direction)
        {
            if (animator)
            {
                animator.SetInteger("WalkX", direction.x < 0 ? -1 : direction.x > 0 ? 1 : 0);
                animator.SetInteger("WalkY", direction.y < 0 ? 1 : direction.y > 0 ? -1 : 0);
            }

        }

        void Update()
        {
            switch (state)
            {
                case State.Idle:
                    IdleState();
                    break;
                case State.Moving:
                    MoveState();
                    break;
                case State.Sitting:
                    SittingAnimate();
                    break;
                case State.Interact:
                    Interact();
                    break;

            }
            //Debug.Log("animator.IsSitting = " + animator.GetBool("IsSitting"));
            //animator.SetBool("IsSitting", true);
        }



      /* void LateUpdate()
        {
            if (pixelPerfectCamera != null)
            {
                transform.position = pixelPerfectCamera.RoundToPixel(transform.position);
            }
        }
      */
        void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            transform.position = startingPosition.initialValue;
       //     pixelPerfectCamera = GameObject.FindObjectOfType<PixelPerfectCamera>();
            
        }


        public void RaiseItem()
        {
            if (playerInventory.currentItem != null)
            {
                if (state != State.Interact)
                {
                    // there's an issue with leaving the interact state here
                    animator.SetBool("ReceiveItem", true);
                    state = State.Interact;
                    //animator.speed = 0;
                    rigidbody2D.velocity = Vector2.zero;
                    receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
                }
                else
                {
                    animator.SetBool("ReceiveItem", false);
                    state = State.Idle;
                    receivedItemSprite.sprite = null;
                    playerInventory.currentItem = null;
                    animator.speed = 1;
                }
            }

        }
        public void SittingAnimate()
        {
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool("IsSitting", true);
            //
            Vector3 sittingPosition1 = new Vector3(-5, 4, 0); 
            gameObject.transform.position = sittingPosition1;
            //
            Debug.Log("should be animating a sitting animation");
        }

        public void SittingState()
        {
            if (animator.GetBool("IsSitting") == false)
            {
                state = State.Sitting;
                animator.SetBool("IsSitting", true);
            }
            else
            {
                state = State.Idle;
                Vector3 standingPosition1 = new Vector3(-5, 3, 0);
                gameObject.transform.position = standingPosition1;
                animator.SetBool("IsSitting", false);
            }
        }

        public void Interact()
        {
            Debug.Log("interact state");
            animator.speed = 0;
            rigidbody2D.velocity = Vector2.zero;

        }

        public void Knock(float knockTime, float damage)
        {
            playerHealthSignal.Raise();
            currentHealth.RuntimeValue -= damage;
            if (currentHealth.RuntimeValue > 0)
            {
                StartCoroutine(KnockCo(knockTime, damage));
            }
            else
            {
                Debug.Log("no cats");
            }
        }

        private IEnumerator KnockCo(float knockTime, float damage)
        {
            {
                yield return new
                WaitForSeconds(knockTime);
                if (state != State.Interact)
                {
                    state = State.Idle;
                }
            }
        }
    }
} 