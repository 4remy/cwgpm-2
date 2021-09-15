using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour
{

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    [Header("Animation")]
    private Animator animator;


    public void NorthChildTrigger()
    {
        Debug.Log("North zone trigger");
        animator.SetBool("north", true);
    }

    public void NorthChildTriggerExit()
    {
        Debug.Log("North zone exit");
        animator.SetBool("north", false);
    }

    public void EastChildTrigger()
    {
        Debug.Log("East zone trigger");
        animator.SetBool("east", true);
    }

    public void EastChildTriggerExit()
    {
        Debug.Log("East zone exit");
        animator.SetBool("east", false);
    }

    public void SouthChildTrigger()
    {
        Debug.Log("South zone trigger");
        animator.SetBool("south", true);
    }

    public void SouthChildTriggerExit()
    {
        Debug.Log("South zone exit");
        animator.SetBool("south", false);
    }

    public void WestChildTrigger()
    {
        Debug.Log("West zone trigger");
        animator.SetBool("west", true);
    }

    public void WestChildTriggerExit()
    {
        Debug.Log("West zone exit");
        animator.SetBool("west", false);
    }

}
