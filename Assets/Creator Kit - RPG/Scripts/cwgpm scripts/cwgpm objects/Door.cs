using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    protected override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange && thisDoorType == DoorType.keyBox)
        {
            Debug.Log("interacted");
            //does player have key
            if(playerInventory.numberOfKeys > 0)
            {
                //remove a player key
                playerInventory.numberOfKeys--;
                //if so, call open method
                Open();
            }

        }

    }
    public void Open()
    {
        //change to different sprite
        animator.SetBool("Open", true);
        //set open to true
        open = true;
        //turn off door box collider if actually door
        //physicsCollider.enabled = false;
    }

    public void Close()
    {

    }


    
}
