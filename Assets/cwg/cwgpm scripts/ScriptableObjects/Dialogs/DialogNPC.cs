using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public enum SpeechType
{
    loopingConvo,
    oneOffConvo
}

public class DialogNPC : Interactable
{

    //reference to the NPC's dialog
    [SerializeField] private Conversation conversation;
    public bool isCompleted;
    // i want to hide onlyOnce from editor
    private bool onlyOnce;

    //this is how the status of your conversation is saved
    public BoolValue convoCompleted;
    public SpeechType thisSpeechType;

    [Header("Paywall locked behind item?")]
    //set unlocked to TRUE if it doesn't require an item.
    public bool unlocked;
    [SerializeField] private Schwer.ItemSystem.Item item1 = default;


    [Header("Do they give you an item?")]
    //set giftless to TRUE if it doesn't give stuff.
    public bool giftless;
    [SerializeField] private Schwer.ItemSystem.Item item2 = default;

  
    [Header("Do they take an item?")]
    //set to noTake TRUE if it doesn't remove any items.
    public bool noTake;
    [SerializeField] private Schwer.ItemSystem.Item item3 = default;

    [Header("Happens independently? untick if you need priors")]
    //set independent to TRUE if you aren't running checks.
    public bool independent;

    [Header("Which prior event is needed?")]

    //do you want your conversation to be triggered only after something else
    // was completed? it can be an earlier convo, but could be any bool value
    //check the bool value is added to your gamesaver manager
    public BoolValue priorEvent;
    public bool priorSuccess;



//issues - more than one conversation on a character?


    private void Start()
    {
        isCompleted = convoCompleted.RuntimeValue;
        priorSuccess = priorEvent.RuntimeValue;

    }
    protected override void Interact()
    {
        if (!independent)
        {
            priorSuccess = priorEvent.RuntimeValue;
            if (!priorSuccess)
            {
                Debug.Log("the prior conversation is set to" + priorSuccess);
                return;
            }
        }
        if (!unlocked)
        {
            if (player.inventory[item1] == 0)
            {
                Debug.Log("you don't have the item needed");
                return;
            }
        }
        if (thisSpeechType == SpeechType.oneOffConvo)
        {
            if (!onlyOnce)
            {
                Debug.Log("one off convo");
                DialogDisplay.NewConversation(conversation);
                //would like this to set the bool as true after the convo finished signal is received 
                isCompleted = true;
                convoCompleted.RuntimeValue = isCompleted;
                onlyOnce = true;
                if (!giftless)
                {
                    player.inventory[item2]++;

                    //I am hard coding these sound effects in
                    AudioManager.Instance.Play("ItemGet");
                }
                if (!noTake)
                {
                    player.inventory[item3]--;

//I am hard coding these sound effects in
//this needs a different sound effect for 'character taking item'

AudioManager.Instance.Play("Whp");
                }
                onlyOnce = true;
            }
            Debug.Log("already did one off convo");
        }
        else
        {
            Debug.Log("looping convo");
            DialogDisplay.NewConversation(conversation);

            // check that the conversation is in progress?

            isCompleted = true;
            convoCompleted.RuntimeValue = isCompleted;
        }

    }
}
