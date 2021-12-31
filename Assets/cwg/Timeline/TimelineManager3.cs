using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager3 : MonoBehaviour
{
    public PlayableDirector director;
    public BoolValue requiredEvent;

    private void Update()
    {

        if (!requiredEvent.RuntimeValue) 
        {
            return;
            
        }
        else
        {
            Debug.Log("Required Event Completed.");
            StartTimeline();

        }
    }

    public void StartTimeline()
    {
        director = GetComponent<PlayableDirector>();
        director.Play();
        print("playable director is playing");
    }
}
