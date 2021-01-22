using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DoorType
{
    keyDoor,
    keyBox,
    button
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    //public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;
    private Animator animator;
    public Item contents;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    protected override void Interact()
    {
        if (playerInventory.numberOfKeys > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerInRange && thisDoorType == DoorType.keyBox)
            {
                if (!open)
                {
                    OpenBox();
                }
                else
                {
                    Close();
                }

            }
            if (Input.GetKeyDown(KeyCode.Space) && playerInRange && thisDoorType == DoorType.keyDoor)
            {
                OpenDoor();
            }
        }
        else
        {
            Close();
        }
    }

    public void OpenDoor()
    {
        //minus one key
        playerInventory.numberOfKeys--;

        //change to different sprite
        animator.SetBool("Open", true);
        //the door needs an animator for open
        open = true;
        //the box collider block is diabled
        physicsCollider.enabled = false;
    }

    public void OpenBox()
    {
        //minus one key
        playerInventory.numberOfKeys--;

        Debug.Log("box should open");
        //dialog on
        dialogBox.SetActive(true);
        // dialog text = contents text;
        dialogText.text = contents.itemDescription;

        //add contents to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //raise the signal to the player
        raiseItem.Raise();

        open = true;

        //change to different sprite
        animator.SetBool("Open", true);
        //set open to true

    }

    public void Close()
    {
        Debug.Log("box closed apparently");
        //turn dialog off
        dialogBox.SetActive(false);
        //raise the signal to the player to stop animating
        raiseItem.Raise();
    }


    
}
