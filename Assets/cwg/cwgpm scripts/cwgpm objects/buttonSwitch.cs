using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonSwitch : Interactable
{
    public bool switchPressed;
    [Header("Animate Conveyor")]
    private Animator animator1;
    [Header("Animate Grinder")]
    private Animator animator2;
    [Header("Hidden unless switch ON")]
    public GameObject hideOnSwitch1;
    public GameObject hideOnSwitch2;

    void Start()
    {
        animator1 = gameObject.transform.GetChild(0).GetComponent<Animator>();
        animator2 = gameObject.transform.GetChild(1).GetComponent<Animator>();

        //subscribe to listen to scene change
        /*SceneManager.activeSceneChanged += ChangedActiveScene;
        {
            Debug.Log("this triggers after the scene changes BACK, or when a scene begins for the first time");
        }
        */
    }

    //this should detect when the scene changes
    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }

        Debug.Log("Scenes: " + currentName + ", " + next.name);
        Debug.Log("turning off the machine if you left it on");
        if (!switchPressed)
            { return; }
            else
            {
               // Debug.Log("the machine was turned on, but you changed scenes, so I'm turning the machine off");
                switchPressed = false;
              
                AudioManager.Instance.Stop("Conveyor");
               // hideOnSwitch1.SetActive(false);
              //  hideOnSwitch2.SetActive(false);
            
        }
    }


    protected override void Interact()
    {
        switchPressed = !switchPressed;
        Debug.Log("button pressed");
        Debug.Log("switch has been set to " + switchPressed);

        if (!switchPressed)
        {
            animator1.SetBool("Switch", false);
            animator2.SetBool("Switch", false);
            AudioManager.Instance.Stop("Conveyor");
            hideOnSwitch1.SetActive(false);
            hideOnSwitch2.SetActive(false);
        }
        else
        {
            animator1.SetBool("Switch", true);
            animator2.SetBool("Switch", true);
AudioManager.Instance.Play("Conveyor");
            hideOnSwitch1.SetActive(true);
            hideOnSwitch2.SetActive(true);
        }
    }
    public void ChildTriggerExit()
    {
      //  Debug.Log("out of machine zone");
        if(!switchPressed)
        { return; }
        else
        {
            switchPressed = false;
            animator1.SetBool("Switch", false);
            animator2.SetBool("Switch", false);
            AudioManager.Instance.Stop("Conveyor");
            hideOnSwitch1.SetActive(false);
            hideOnSwitch2.SetActive(false);
        }
        
    }
}
