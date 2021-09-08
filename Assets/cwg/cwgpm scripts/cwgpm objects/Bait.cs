using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SchwerInventory = Schwer.ItemSystem.Inventory;
using UnityEngine.UI;

public class Bait : MonoBehaviour
{
    // [SerializeField] private Schwer.ItemSystem.Item item = default;


    //i think interact isnt working because of the triggers

    public Signal BaitSignal;
    public bool baitZone;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && baitZone)
        {
            Debug.Log("you have interacted lol");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log("player in bait zone");
            baitZone = true;
        Debug.Log("baitZone " + " is " + baitZone);

    }

    public void OnTriggerExit2D(Collider2D other)
    {
            Debug.Log("player has exited bait zone");
            baitZone = false;
            Debug.Log("baitZone" + " is " + baitZone);
    }


    public void Use()
    {
        //it never registers as in range
        BaitSignal.Raise();
        Debug.Log("signal generated");
        //doesnt say anything if no signal received.
    }

    public void baitRecieved()
    {
        Debug.Log("code can recieve signal");
        Debug.Log("baitZone " + " is genuinely " + baitZone);
        if (!baitZone)
        {
            Debug.Log("signal recieved");
            Debug.Log("not in range tho");
        }
        else
        {
            Debug.Log("signal recieved");
            Debug.Log("Dana voice: lets goo");
        }

    }
}