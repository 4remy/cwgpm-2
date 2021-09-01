using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            FindObjectOfType<AudioManager>().Stop("Conveyor");
            hideOnSwitch1.SetActive(false);
            hideOnSwitch2.SetActive(false);
        }
        else
        {
            animator1.SetBool("Switch", true);
            animator2.SetBool("Switch", true);
FindObjectOfType<AudioManager>().Play("Conveyor");
            hideOnSwitch1.SetActive(true);
            hideOnSwitch2.SetActive(true);
        }
    }
}
