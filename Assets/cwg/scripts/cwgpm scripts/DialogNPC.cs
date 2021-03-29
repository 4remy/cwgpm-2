using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNPC : Interactable
{

    //reference to the NPC's dialog
    [SerializeField] private Conversation myConversation;
    public Conversation conversation;
    //notification to send to the canvases to activate and check dialog
    public Signal DialogNotification;

    protected override void Interact()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            Debug.Log("interacting");
            DialogNotification.Raise();
        }
        else
        {
            DialogNotification.Raise();
        }
    }
}
