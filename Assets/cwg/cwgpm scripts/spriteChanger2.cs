using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spriteChanger2 : Interactable
{
    public BehaviourType thisBehaviourType;


    private bool handCompleted;
    public BoolValue handConvoCompleted;

    [Header("Animation")]
    private Animator animator;

    void Start()
    {
        handCompleted = handConvoCompleted.RuntimeValue;
        animator = GetComponent<Animator>();
        if (handCompleted)
        {
            animator.SetBool("hand", true);
        }
    }

    protected override void Interact()
    {

        handCompleted = handConvoCompleted.RuntimeValue;
        Debug.Log("handCompleted " + " is  " + handCompleted);


            if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
            {
                if (!handCompleted)
                {
                    return;
                }
                else
                {
                    animator.SetBool("hand", true);
                   // AudioManager.Instance.Play("itemgive");
                    Debug.Log("handCompleted " + " is  " + handCompleted);
                }
            }
        else
        {
            return;
        }
    }


}
