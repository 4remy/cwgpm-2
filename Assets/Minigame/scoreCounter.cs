using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class scoreCounter : MonoBehaviour
{
    static public int score = 0; //player score
    public Text uiText;

    void Start()
    {
        FindObjectOfType<itemCollisions>().LandedEvent += onLandedEvent;
    }

    void Awake()
    {
        uiText = GetComponent<Text>();  //Set up the reference.
        score = 0;  //Reset the score.
    }

    void Update()
    {
        uiText.text = "Score: " + score;    // Set the displayed text to be the word "Score: " followed by the score value.
    }

    public void onLandedEvent()
    {
        FindObjectOfType<itemCollisions>().LandedEvent -= onLandedEvent;
        // Debug.Log("The item has landed!!");
        //scoreCounter.score += 10;
        scoreCounter.score += toppingFall.IngredientValue;

        //unity event carries info about what type of item/how many points?
    }
}
