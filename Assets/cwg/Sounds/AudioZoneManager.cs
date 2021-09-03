using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// you make an audio zone manager for each separate zone
// it is too messy to add more than one collider on this
//vcams have separate rooms too after all
public class AudioZoneManager : MonoBehaviour
{
    public string Theme;
    //private string TheCollider1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterController2D>();
        if (player != null)
        {
            FindObjectOfType<AudioManager>().PlayTheme(Theme);
            Debug.Log("theme should play btw");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterController2D>();
        if (player != null)
        {
            FindObjectOfType<AudioManager>().Fade(Theme);
           // FindObjectOfType<AudioManager>().Stop(Theme1);
            Debug.Log("theme should stop btw");
        }
    }
}
