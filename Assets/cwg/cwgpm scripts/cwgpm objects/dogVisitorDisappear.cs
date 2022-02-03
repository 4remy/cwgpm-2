using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogVisitorDisappear : MonoBehaviour
{
    //if the convo was completed on a previous restaurant load, the dog visitor won't show up this time

    public BoolValue requiredEvent;

    [SerializeField] private IntListSO discoveredRecipes = default;
    private bool milestone1;

    public int magicNumber1;

    // Start is called before the first frame update
    void Start()
    {
        if (!requiredEvent.RuntimeValue)
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
            if (!milestone1)
            {
                Debug.Log("Milestone " + gameObject.name + " is " + milestone1);
                this.gameObject.SetActive(false);

                return;
            }
            else
            {
                Debug.Log("Milestone " + gameObject.name + " is " + milestone1);
                return;
            }
        }
        else
        {
           //if convo already completed, no dog shown at scene load
            this.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
