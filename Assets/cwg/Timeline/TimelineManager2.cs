using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Playables;



public class TimelineManager2 : MonoBehaviour

    //cutscene triggered on transition to new scene
    //not triggered by interaction

    //this cutscene is only played once, and when it is completed, the value is stored in game save manager.

    /*you can use the inventory on an interactable type script, or a script that involves entering a box collider
    so if you're giving an item to someone and that creates a whole cutscene that's more complicated than a dialogue, that's when you would use a type three timeline manager
    */
{
    public PlayableDirector director;
    public BoolValue cutsceneCompleted;
    public bool isCompleted;



    //i guess I would test this on coming outside with the catnip bool achievement

    [Header("Which prior event is needed?")]

    //do you want your conversation to be triggered only after something else
    // was completed? it can be an earlier convo, but could be any bool value
    //check the bool value is added to your gamesaver manager

    public BoolValue priorEvent;
    public bool priorSuccess;


    private void Awake()
    {
        //checks for conditions once at start of scene

        isCompleted = cutsceneCompleted.RuntimeValue;
        priorSuccess = priorEvent.RuntimeValue;

       if(!isCompleted)
        {
            priorSuccess = priorEvent.RuntimeValue;
            if (!priorSuccess)
            {
                Debug.Log("the prior requirement is set to" + priorSuccess);
                return;

            }
            else
            {
                StartTimeline();
            }

        }
       else
        {
            Debug.Log("cutscene already completed");
        }
       
    }


    public void StartTimeline()
    {
        director = GetComponent<PlayableDirector>();
        isCompleted = true;
        cutsceneCompleted.RuntimeValue = isCompleted;
        director.Play();
        
        print("playable director is playing");
    }
}


