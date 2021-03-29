using UnityEngine;

public class DialogNPC : Interactable
{
    //reference to the NPC's dialog
    [SerializeField] private Conversation conversation;

    protected override void Interact()
    {
        Debug.Log("interacting");
        DialogDisplay.NewConversation(conversation);
    }
}
