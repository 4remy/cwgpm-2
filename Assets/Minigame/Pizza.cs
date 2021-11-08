using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pizza : MonoBehaviour
{
    public string soundEffectToPlay1;
    public string soundEffectToPlay2;
    public string soundEffectToPlay3;

    private int randomMusic;

    public bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        //it needs to count the NUMBER of items on it
       
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<toppingFall>())
        {
          /*
           * 
           * this has a problem with the type being assigned as enum
           * I think it might be best to have it be a type
           * confused by getters and setters tho
            if (toppingFall.thisIngredientType == toppingFall.thisIngredientType.egg)
            {
                scoreCounter.score += toppingFall.IngredientValue;


                StartCoroutine(playSound2Co());

                return;
            }
          */

            //if ingredient type is special play different sound, and score and return

            scoreCounter.score += toppingFall.IngredientValue;


            // StartCoroutine(playSound1Co());

            ChooseMusic();

            // Debug.Log("Score is " + scoreCounter.score);
            //Debug.Log("Ingredient type is " + toppingFall.thisIngredientType);

            //advanced: play a sound according to ingredient type?
        }

    }

    public void ChooseMusic()
    {
        randomMusic = Random.Range(0, 3);

        switch (randomMusic)
        {
            case 0:
                StartCoroutine(playSound1Co());
                Debug.Log("random1");
                break;
            case 1:
                StartCoroutine(playSound2Co());
                Debug.Log("random2");
                break;
            case 2:
                StartCoroutine(playSound3Co());
                Debug.Log("random3");
                break;
        }

    }
        IEnumerator playSound1Co()

    {
        if(!isPlaying)
        {
            
            isPlaying = true;
            AudioManager.Instance.Play(soundEffectToPlay1);
            yield return new WaitForSeconds(1f);
            isPlaying = false;
        }
        
    }

    
    IEnumerator playSound2Co()

    {
        if (!isPlaying)
        {
            isPlaying = true;
            AudioManager.Instance.Play(soundEffectToPlay2);
            yield return new WaitForSeconds(1f);
            isPlaying = false;
        }

    }

    IEnumerator playSound3Co()

    {
        if (!isPlaying)
        {
            isPlaying = true;
            AudioManager.Instance.Play(soundEffectToPlay3);
            yield return new WaitForSeconds(1f);
            isPlaying = false;
        }

    }

}
