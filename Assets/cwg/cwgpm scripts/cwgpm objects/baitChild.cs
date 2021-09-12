using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baitChild : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (tag == "bait")
        {
            return; // do nothing
        }
        else
        {
            gameObject.GetComponentInParent<Bait>().ChildTrigger();
            //Debug.Log("main zone triggered!");
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponentInParent<Bait>().ChildTriggerExit();
        //Debug.Log("main zone left!");
    }
}