using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pizza : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<itemCollisions>().LandedEvent += onLandedEvent;
    }

    // Update is called once per frame
    void Update()
    {
        //it needs to count the NUMBER of items on it
        
    }

    
    public void onLandedEvent()
    {
        FindObjectOfType<itemCollisions>().LandedEvent -= onLandedEvent;
       //code goes on the pizza
        scoreCounter.score += toppingFall.IngredientValue;
        // Debug.Log("Score is " + scoreCounter.score);
        Debug.Log("Ingredient type is " + toppingFall.thisIngredientType);
        //return;

        //you should maybe assign the types of topping a value here instead??

        //this only registers once for each type of object. it doesn't register the new ones cloned by instantiator after the first egg and first tomato get points.
    }
    
}
