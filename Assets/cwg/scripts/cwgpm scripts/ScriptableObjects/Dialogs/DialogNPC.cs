using UnityEngine;
using UnityEngine.UI;

public enum SpeechType
{
    loopingConvo,
    //change to loopingConvo
    linearConvo
        //change to oneOffConvo
        //add dependentConvo
        //then check the the required conversation boolvalue is true, otherwise print a debug log that it the required convo hadn't been completeld
}

public class DialogNPC : Interactable
{
    //reference to the NPC's dialog
    [SerializeField] private Conversation conversation;
    public bool isCompleted;
    public BoolValue convoCompleted;
    public SpeechType thisSpeechType;
    //do you want your conversation to pre-require another convo before it?
    [Header("Requirements")]
    public bool requiresPrior;
    //stick something random in here,
    //when you don't have this turned on
    //because I don't know how to remove need for these
    //when requiresBool is turned off.
    public bool requiredToBeCompleted;
    public BoolValue requiredCompleted;

    //how do you make it wait a coroutine if it requires a prior, and the prior is also on the same character
    //maybe it would work good 2 convos on same character

    private void Start()
    {
        isCompleted = convoCompleted.RuntimeValue;
        requiredToBeCompleted = requiredCompleted.RuntimeValue;

    }
    protected override void Interact()
    {
        if (requiresPrior)
        {
            if(requiredToBeCompleted)
            {
                if (thisSpeechType == SpeechType.linearConvo)
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
                else
                {
                    Debug.Log("interacting");
                    DialogDisplay.NewConversation(conversation);
                }
            }
            Debug.Log("hasn't completed required prior conversation");
        }
        else
        //check BoolValue for convoCompleted is true
        //if it is, do the below
        if (thisSpeechType == SpeechType.linearConvo)
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
       //else (thisSpeechType == SpeechType.loopingConvo)
       else
        {
            Debug.Log("interacting");
            DialogDisplay.NewConversation(conversation);
        }

    }


    //Debug.Log("hasn't completed required prior conversation");
}

