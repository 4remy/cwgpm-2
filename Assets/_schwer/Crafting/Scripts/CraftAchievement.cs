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
    public bool canDismiss;


    [Header("Animation")]
    public Animator effect;

    public string soundEffectToPlay;

    //animator for the effect

    // Start is called before the first frame update
    void Start()
    {
        effect = gameObject.transform.GetChild(1).GetComponent<Animator>();

        effect.SetBool("Effect", false);

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
            {
                Debug.Log("Milestone " + gameObject.name + " is " + milestone1);
                achievePanel.SetActive(true);
                dialogText.text = dialogAchieve1;
                StartCoroutine(AchieveCo());
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) && canDismiss)
                {
                    effect.SetBool("Effect", false);
                    achievePanel.SetActive(false);
                }
            }
        }
    }

    IEnumerator AchieveCo()
    {
        achieveShown = true;
        storedAchievement1.RuntimeValue = achieveShown;
        Debug.Log("you can't exit yet");
        effect.SetBool("Effect", true);
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.Play(soundEffectToPlay);
        canDismiss = true;
        Debug.Log("you can exit the event now");

       // effect.SetBool("Effect", false);
       // achievePanel.SetActive(false);

    }

}
