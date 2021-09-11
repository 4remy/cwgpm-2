using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a pot breaks when you press the space key.

public class trash : Interactable
{


    //override interact, to say what happens during an interaction for this object

    protected override void Interact()
    {
        Debug.Log("interacting");
        StartCoroutine(BreakCo());
    }

    //this coroutine is an add the action that interact triggers

    private IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}