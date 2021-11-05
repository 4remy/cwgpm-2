using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class instantiator : MonoBehaviour
{
    public float minWait;
    public float maxWait;

    private bool isSpawning;
    private bool gameOver;

    public GameObject[] objectsToInstantiate;

    void Awake()
    {
        isSpawning = false;
        FindObjectOfType<Timer>().TimeUpEvent += onTimeUpEvent;
    }

    void Update()
    {
        if (!gameOver)
        {
            if (!isSpawning)
            {
                float timer = Random.Range(minWait, maxWait);
                Invoke("SpawnObject", timer);
                isSpawning = true;
            }
        }

    }

    void SpawnObject()
    {
        if (!gameOver)
        {
            int n = Random.Range(0, objectsToInstantiate.Length);
            Instantiate(objectsToInstantiate[n]);
            isSpawning = false;
        }
    }

    public void onTimeUpEvent()
    {
        FindObjectOfType<Timer>().TimeUpEvent -= onTimeUpEvent;
        gameOver = true;
       
        Debug.Log("Items will not instantiate now that time is up.");
    }
}