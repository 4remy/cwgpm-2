using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machineBaitChild : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        return;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponentInParent<buttonSwitch>().ChildTriggerExit();
        //Debug.Log("main zone left!");
    }
}