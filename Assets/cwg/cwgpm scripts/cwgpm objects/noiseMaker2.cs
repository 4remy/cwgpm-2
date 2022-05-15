using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make a noise when interact
// this has no wait time

public class noiseMaker2 : MonoBehaviour
{
    public bool playerInRange;
    public string soundEffectToPlay;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            AudioManager.Instance.Play(soundEffectToPlay);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


}
