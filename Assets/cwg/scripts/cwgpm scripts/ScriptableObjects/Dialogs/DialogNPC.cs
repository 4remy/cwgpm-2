using UnityEngine;
using UnityEngine.UI;

public enum NPCType
{
    loopingTalker,
    linearTalker
}

public class DialogNPC : Interactable
{
    //reference to the NPC's dialog
    [SerializeField] private Conversation conversation;
    public bool isCompleted;
    public BoolValue convoCompleted;
    public NPCType thisNPCType;

    private void Start()
    {
        isCompleted = convoCompleted.RuntimeValue;
    }
    protected override void Interact()
    {
        if (thisNPCType == NPCType.linearTalker)
        {
            if (!isCompleted)
            {
                Debug.Log("interacting");
                DialogDisplay.NewConversation(conversation);
                isCompleted = true;
                convoCompleted.RuntimeValue = isCompleted;
            }
            else
            {
                Debug.Log("finished Talking");
            }
        }



       //
       //else (thisNPCType == NPCType.loopingTalker)
       else
        {
            Debug.Log("interacting");
            DialogDisplay.NewConversation(conversation);
        }

    }


}

