using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthWatchZone : MonoBehaviour
{
   // public string childName;

    void OnTriggerEnter2D(Collider2D other)
    {
            gameObject.GetComponentInParent<Watcher>().NorthChildTrigger();
           // Debug.Log("watch zone triggered!");


    }

    void OnTriggerExit2D(Collider2D other)
    {
 gameObject.GetComponentInParent<Watcher>().NorthChildTriggerExit();
           // Debug.Log("watch zone left!");

    }
}