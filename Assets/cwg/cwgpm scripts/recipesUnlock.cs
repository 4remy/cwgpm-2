﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recipesUnlock : MonoBehaviour
{
    [SerializeField] private IntListSO discoveredRecipes = default;
   private bool milestone1;
   public GameObject unlockMe;

    public int magicNumber1;

    // Start is called before the first frame update
    void Start()
{
        var recipeCount = discoveredRecipes.ints.Count;
        if (recipeCount >= magicNumber1)
        {
            milestone1 = true;
        }
        else
        {
            milestone1 = false;
        }
        if(!milestone1)
        {
            Debug.Log("Milestone " + gameObject.name + " is " + milestone1);
            unlockMe.SetActive(false);
            return;
        }
        else
        {
            Debug.Log("Milestone " + gameObject.name + " is " + milestone1);
            unlockMe.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
