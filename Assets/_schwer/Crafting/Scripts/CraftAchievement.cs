using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftAchievement : MonoBehaviour
{
    [SerializeField] private IntListSO discoveredRecipes = default;
    private bool milestone1;

    private bool HasCraftedFiveRecipes;
    private int recipeCount;
    public int magicNumber1;

    public GameObject achievePanel;

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


    }

    // Update is called once per frame
    void Update()
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
            achievePanel.SetActive(false);
            return;
        }
        else
        {
            Debug.Log("Milestone " + gameObject.name + " is " + milestone1);
            achievePanel.SetActive(true);
        }
    }
}
