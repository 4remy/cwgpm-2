using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class greenhouseItem : MonoBehaviour
{


    [Header("Random GameObjects")]
    public GameObject[] myObjects;
    private bool randomItemChanged = false;

    public GameObject chosenItem;



    void Awake()
    {
        chosenItem = myObjects[UnityEngine.Random.Range(0, myObjects.Length)];

        chosenItem.SetActive(true);

        randomItemChanged = true;
    }

}
