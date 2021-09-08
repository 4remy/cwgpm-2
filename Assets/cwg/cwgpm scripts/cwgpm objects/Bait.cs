using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SchwerInventory = Schwer.ItemSystem.Inventory;
using UnityEngine.UI;

public class Bait : MonoBehaviour
{
    // [SerializeField] private Schwer.ItemSystem.Item item = default;


    //i think interact isnt working because of the triggers

    public bool baitZone;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && baitZone)
        {
            Debug.Log("you have interacted lol");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log("player in bait zone");
            baitZone = true;
        Debug.Log("baitZoneis " + gameObject.name + " is " + baitZone);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
            Debug.Log("player has exited bait zone");
            baitZone = false;
            Debug.Log("baitZoneis " + gameObject.name + " is " + baitZone);
    }


    public void Use()
    {
        Debug.Log("you have pressed use lol");
    }

}