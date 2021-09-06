using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recipesUnlock : MonoBehaviour
{
    [SerializeField] private IntListSO discoveredRecipes = default;
   private bool milestone1;
   public GameObject unlockMe;

    private bool HasCraftedFiveRecipes()
    {
        var recipeCount = discoveredRecipes.ints.Count;
        milestone1 = true;
        return recipeCount >= 5;
}
    // Start is called before the first frame update
    void Start()
    {

        if(!milestone1)
        {
            Debug.Log("milestone1 is " + milestone1);
            unlockMe.SetActive(false);
            return;
        }
        else
        {
            Debug.Log("milestone1 is " + milestone1);
            unlockMe.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
