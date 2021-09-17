using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class unlockedDoor : Interactable
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
                OpenDoor();
            }
        else
        {
            return;
        }
       
    }

    public void OpenDoor()
    {
        physicsCollider.enabled = false;
        open = true;
        //change to different sprite
        animator.SetBool("Open", true);
        Debug.Log("door Opened");

        //the door needs an animator for open
        
        //the box collider block is diabled

    }





}
