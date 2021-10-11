using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class greenhouseDoor : Interactable
{
    public bool open;


    public BoxCollider2D physicsCollider;

    [Header("Animation")]
    private Animator animator;

    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
        if (open)
        {
            animator.SetBool("Open", true);
            physicsCollider.enabled = false;
        }


    }

    protected override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            Debug.Log("open door called!");
            Door();
        }
        else
        {
            return;
        }

    }

    public void Door()
    {
        open = !open;

        if (!open)
        {
            physicsCollider.enabled = true;
            //change to different sprite
            animator.SetBool("Open", false);
            Debug.Log("door shut");
        }
        else
        {
            physicsCollider.enabled = false;
            //change to different sprite
            animator.SetBool("Open", true);
            Debug.Log("door Opened");

        }

        //the door needs an animator for open

        //the box collider block is diabled

    }





}
