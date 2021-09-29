using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftAchievement : MonoBehaviour
{
    [SerializeField] private IntListSO discoveredRecipes = default;
    private bool milestone1;
    public int magicNumber1;

    public BoolValue storedAchievement1;

    public GameObject achievePanel;
    public Text dialogText;
    [Multiline]
    public string dialogAchieve1;
    public bool achieveShown;

    //animator for the effect

    // Start is called before the first frame update
    void Start()
    {
        achieveShown = storedAchievement1.RuntimeValue;

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
        // maybe change this to onclick instead

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
            if(!achieveShown)
            //swap this out for storedAchievement1
            //test inbetween 2 play sessions
            {
                Debug.Log("Milestone " + gameObject.name + " is " + milestone1);
                achievePanel.SetActive(true);
                dialogText.text = dialogAchieve1;
                StartCoroutine(AchieveCo());
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && achieveShown)
                {
                    achievePanel.SetActive(false);
                }
            }
        }
    }

    IEnumerator AchieveCo()
    {
        Debug.Log("you can't exit yet");
        //animation goes here
        yield return new WaitForSeconds(5f);
        Debug.Log("you can exit the event now");
        achieveShown = true;
        storedAchievement1.RuntimeValue = achieveShown;

    }

    //coroutine
    //animation
    //for set amount of time
    //on space press make the achievement panel not active
}
