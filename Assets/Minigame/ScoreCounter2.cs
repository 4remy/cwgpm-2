using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreCounter2 : MonoBehaviour
{
     //player score
    public Text uiText;


    void Awake()
    {
        uiText = GetComponent<Text>();  //Set up the reference.
         

        //FindObjectOfType<Timer>().TimeUpEvent += onTimeUpEvent;
    }

    void Update()
    {
        uiText.text = " " + scoreCounter.score;    // Set the displayed text to be the word "Score: " followed by the score value.

    }
    /*
    public void onTimeUpEvent()
    {
        uiText.color = Color.red;
    }
    */

}
