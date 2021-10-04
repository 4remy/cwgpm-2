using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector director;
    public bool firstPlay;
    public BoolValue storedFirstPlay;
    private bool sceneSkipped = true;
    private float timeToSkipTo;

    // Start is called before the first frame update
    void Start()
    {
        sceneSkipped = false;
        firstPlay = storedFirstPlay.RuntimeValue;
        Debug.Log("the timeline manager is awake btw.");

        if (!firstPlay) //return true if the key exist
        {
            Debug.Log("First time in the game.");
            firstPlay = true;
            storedFirstPlay.RuntimeValue = firstPlay;
            director = GetComponent<PlayableDirector>();
            director.Play();
        }
        else
        {
            Debug.Log("It is not the first time in the game.");
            director.Stop();
            sceneSkipped = true;
            director.time = 90f;
            // this.gameObject.SetActive(false);
            return;
        }

    }

    public void GetSkipTime(float skipTime)
    {
        timeToSkipTo = skipTime;
    }




}
