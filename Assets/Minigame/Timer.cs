using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Timer : MonoBehaviour
{
    static public float timeRemaining = 10;
    public float setTimeRemaining;

    public bool timerIsRunning = false;
    public Text timeText;

    [Header("GameOverPanel")]
    public GameObject timeUpPanel;
    //the thing with animator must be first child of panel
   // public Animator effect;
   // public GameObject animationObject;

    public delegate void TimeOverDelegate();
    public event TimeOverDelegate TimeUpEvent;

    void Awake()
    {
        timeRemaining = setTimeRemaining;
    }

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        //  effect = gameObject.transform.GetChild(1).GetComponent<Animator>();
        //effect.SetBool("Effect", false);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                timeText.color = Color.red;

               // effect.SetBool("Effect", true);
                timeUpPanel.SetActive(true);
               //animationObject.SetActive(true);
               //add IEnumerator from CraftAchievement if you don't want it to last

                if (TimeUpEvent != null)
                {
                    TimeUpEvent();
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}