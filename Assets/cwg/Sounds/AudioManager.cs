using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    Dictionary<string, Coroutine> fades = new Dictionary<string, Coroutine>();

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
        //turns volume on
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

        if (fades.ContainsKey(name))
        { // if this sound is already being faded
          //  StopCoroutine(fades[name]); // stop fading it
            fades.Remove(name);

            //this bit makes it break lol
        }

        // start a new fade operation
        Coroutine fadeCoroutine = StartCoroutine(fadeCo(s, 1.5f, name));
        // track it in the map
    fades.Add(name, fadeCoroutine);



    }

    IEnumerator fadeCo(Sound sound, float durationSeconds, string name)
    {
        float startTime = Time.time;
        float endTime = startTime + durationSeconds;
        float startVolume = sound.source.volume;

        while (sound.source.volume > 0)
        {
            // a value that goes from 1 to 0 in durationSeconds time
            float interp = Mathf.Max(0, (endTime - Time.time) / durationSeconds);

            // the sound will go from the start volume to 0 after durationSeconds pass
            sound.source.volume = startVolume * interp;
            yield return null;
        }
    }

    /*
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

IEnumerator fadeCo(Sound sound, float durationSeconds, string name) {
    float startTime = Time.time;
    float endTime = startTime + durationSeconds;
    float startVolume = sound.volume;

    while (sound.volume > 0) {
        // a value that goes from 1 to 0 in durationSeconds time
        float interp = Mathf.Max(0, (endTime - Time.time) / durationSeconds);

        // the sound will go from the start volume to 0 after durationSeconds pass
        sound.volume = startVolume * interp;
        yield return null;
    }
     */
}

/* this does something really fucking cool and weird
s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume/ 2f));
s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch / 2f, s.pitch / 2f));
*/
