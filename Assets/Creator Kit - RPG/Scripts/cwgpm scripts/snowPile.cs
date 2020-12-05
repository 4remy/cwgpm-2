using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class snowPile : Interactable
{
    public Item contents;
    public bool isDug;
    public Signal raiseItem;
    public Inventory playerInventory;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    protected override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if(!isDug)
            {
                DigSnow();
            }
            else
            {
                SnowDug();
            }
        }

    }

    public void DigSnow()
    {
        //dialog on
        dialogBox.SetActive(true);
       // dialog text = contents text;
        dialogText.text = contents.itemDescription;

        //add contents to the inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //raise the signal to the player
        raiseItem.Raise();
        //set the snow to dug
        isDug = true;
        animator.SetBool("isDug", true);

    }
    public void SnowDug()
    {
       //turn dialog off
       dialogBox.SetActive(false);
       //raise the signal to the player to stop animating
       raiseItem.Raise();
        
    }
}
