using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pizza : MonoBehaviour
{
    public string soundEffectToPlay;
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
            //if ingredient type is special play different sound, and score and return

            scoreCounter.score += toppingFall.IngredientValue;


            StartCoroutine(playSound1Co());

            // Debug.Log("Score is " + scoreCounter.score);
            //Debug.Log("Ingredient type is " + toppingFall.thisIngredientType);

            //advanced: play a sound according to ingredient type?
        }

    }

    IEnumerator playSound1Co()

    {
        if(!isPlaying)
        {
            isPlaying = true;
            AudioManager.Instance.Play(soundEffectToPlay);
            yield return new WaitForSeconds(1f);
            isPlaying = false;
        }
        
    }


}
