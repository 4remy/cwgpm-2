using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthWatchZone : MonoBehaviour
{
    // public string childName;

    void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.GetComponentInParent<Watcher>().SouthChildTrigger();
        // Debug.Log("watch zone triggered!");


    }

    void OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponentInParent<Watcher>().SouthChildTriggerExit();
        // Debug.Log("watch zone left!");

    }
}