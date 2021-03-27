using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNPC : Interactable
{
    [SerializeField] private TextAssetValue dialogValue;
    //reference to the NPC's dialog
    [SerializeField] private TextAsset myDialog;
    //notification to send to the canvases to activate and check dialog
    [SerializeField] private Signal branchingDialogNotification;

    protected override void Interact()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            // Debug.Log("interacting");
            dialogValue.value = myDialog;
            branchingDialogNotification.Raise();
        }
    }
}
