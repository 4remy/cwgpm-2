using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EastWatchZone : MonoBehaviour
{
   // public string childName;

    void OnTriggerEnter2D(Collider2D other)
    {
            gameObject.GetComponentInParent<Watcher>().EastChildTrigger();
           // Debug.Log("watch zone triggered!");


    }

    void OnTriggerExit2D(Collider2D other)
    {
 gameObject.GetComponentInParent<Watcher>().EastChildTriggerExit();
           // Debug.Log("watch zone left!");

    }
}