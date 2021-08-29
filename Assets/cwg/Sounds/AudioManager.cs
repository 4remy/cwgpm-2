using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
     
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
/*
    void Start()
    {
        //change theme according to scene
        Play("Theme");
        Debug.Log("theme should play btw");
    }
*/

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        s.source.volume = s.volume * (1);
        if (s == null)
            Debug.Log("sound not found");
            return;

    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
            return;
        }

        //make sound stop  IMMEDIATELY after leaving zone
        s.source.volume = s.volume * (0);
        // how to make sound fade out?
        

    }

    public void Fade(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound not found");
            return;
        }
        StartCoroutine(fadeCo());
    }

    IEnumerator fadeCo()
    {
        //make it fade here
        Debug.Log("this would fade if there was code");
        yield break;
    }
}
/* this does something really fucking cool and weird
s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume/ 2f));
s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch / 2f, s.pitch / 2f));
*/
