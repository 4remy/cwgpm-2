using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pizza2 : MonoBehaviour
{
    public string soundEffectToPlay;
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
        if (collision.GetComponent<BadTopping>())
        {

            scoreCounter.score -= BadTopping.BadIngredientValue;

            AudioManager.Instance.Play(soundEffectToPlay);

            // Debug.Log("Score is " + scoreCounter.score);
            //Debug.Log("Ingredient type is " + toppingFall.thisIngredientType);

        }

    }
}
