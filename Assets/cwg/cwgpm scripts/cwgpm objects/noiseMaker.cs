using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make a noise when interact

public class noiseMaker : MonoBehaviour
{
    public bool playerInRange;
    public string soundEffectToPlay;
    private bool soundPlaying;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
           if(soundPlaying)
            {
                return;
            }
           else
            {
                StartCoroutine(NoiseCo());
            }
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

    private IEnumerator NoiseCo()
    {
        soundPlaying = true;
        AudioManager.Instance.Play(soundEffectToPlay);
        yield return new WaitForSeconds(1.3f);
        soundPlaying = false;
    }
}
