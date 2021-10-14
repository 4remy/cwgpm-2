using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManagerRecipes : MonoBehaviour
{
    public PlayableDirector director;

    [SerializeField] private IntListSO discoveredRecipes = default;
    private bool milestone1;

    public int magicNumber1;


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
        if (!milestone1)
        {
            Debug.Log("Milestone " + gameObject.name + " is " + milestone1);
            return;
        }
        else
        {
            Debug.Log("Milestone " + gameObject.name + " is " + milestone1);
            director = GetComponent<PlayableDirector>();
            director.Play();
        }

    }

}
