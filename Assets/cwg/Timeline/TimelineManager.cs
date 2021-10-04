using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector director;
    public bool firstPlay;
    public BoolValue storedFirstPlay;

    private void Awake()
    {
        Debug.Log("the timeline manager is awake btw.");

        if (!firstPlay) //return true if the key exist
        {
            Debug.Log("First time in the game.");
            firstPlay = true;
            storedFirstPlay.RuntimeValue = firstPlay;
            StartTimeline();
        }
        else
        {
            Debug.Log("It is not the first time in the game.");

            //director.Stop();
            return;
        }
    }

    public void StartTimeline()
    {
        director = GetComponent<PlayableDirector>();
        director.Play();
        print("playable director is playing");
    }
}
