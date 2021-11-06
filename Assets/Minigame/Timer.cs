﻿using System.Collections;
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