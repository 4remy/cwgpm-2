using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pizza : MonoBehaviour
{
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

            scoreCounter.score += toppingFall.IngredientValue;
            // Debug.Log("Score is " + scoreCounter.score);
            //Debug.Log("Ingredient type is " + toppingFall.thisIngredientType);

            // play a sound
            //advanced: play a sound according to ingredient type?
        }

    }
}
