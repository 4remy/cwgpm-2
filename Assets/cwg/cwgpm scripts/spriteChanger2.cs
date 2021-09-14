using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spriteChanger2 : Interactable
{
    public BehaviourType thisBehaviourType;


    private bool completeHand;
    public BoolValue handConvoCompleted;

    public bool isGiven;
    public BoolValue storedHandGiven;

    [Header("Animation")]
    private Animator animator;

    void Start()
    {
        completeHand = handConvoCompleted.RuntimeValue;
        animator = GetComponent<Animator>();
        if (completeHand)
        {
            animator.SetBool("hand", true);
        }
    }

    protected override void Interact()
    {

        completeHand = handConvoCompleted.RuntimeValue;
        Debug.Log("handCompleted " + " is  " + completeHand);


            if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
            {
                if (!completeHand)
                {
                    return;
                }
                else
                {
                    if(!isGiven)
                    {
                        AudioManager.Instance.Play("itemgive");
                    }
                    animator.SetBool("hand", true);
                    Debug.Log("handCompleted " + " is  " + completeHand);
                    isGiven = true;
                    storedHandGiven.RuntimeValue = isGiven;
                }
            }
        else
        {
            return;
        }
    }


}
