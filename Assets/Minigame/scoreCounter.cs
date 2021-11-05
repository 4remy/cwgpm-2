using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class scoreCounter : MonoBehaviour
{
    static public int score = 0; //player score
    public Text uiText;


    void Awake()
    {
        uiText = GetComponent<Text>();  //Set up the reference.
        score = 0;  //Reset the score.
    }

    void Update()
    {
        uiText.text = "Score: " + score;    // Set the displayed text to be the word "Score: " followed by the score value.
        
    }


}
