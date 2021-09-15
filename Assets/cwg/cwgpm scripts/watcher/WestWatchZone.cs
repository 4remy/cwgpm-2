using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestWatchZone : MonoBehaviour
{
    // public string childName;

    void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.GetComponentInParent<Watcher>().WestChildTrigger();
        // Debug.Log("watch zone triggered!");


    }

    void OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponentInParent<Watcher>().WestChildTriggerExit();
        // Debug.Log("watch zone left!");

    }
}
