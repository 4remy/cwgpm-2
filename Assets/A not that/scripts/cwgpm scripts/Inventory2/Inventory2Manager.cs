using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory2Manager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory2 playerInventory2;
    //
    [SerializeField] private ItemDatabase itemDatabase;
    //
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanelContent;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public Inventory2Item currentItem;
    //
    //
    private List<int> saveIDList = new List<int>();
    //
    //
    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        if(buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    void MakeInventorySlots()
    {
        if(playerInventory2)
        {//
         //
         /*
            saveIDList.Clear(); //To do clean state. 
            saveIDList = LoadIDList("player_inventory"); //Because the LoadIDList is of return type and it returns List<int> value. So, we can do this.

            //If saveIDList got no list at all then by all means stop this method and continue with something else.
            if (saveIDList.count <= 0)
                return;
            */
            //
            //
            //

            for (int i = 0; i < playerInventory2.myInventory.Count; i++)
            {
                if (playerInventory2.myInventory[i].numberHeld > 0)
                    //for objects to keep displaying at 0
                    //paste the below before the ')' sign above
                    // || playerInventory.myInventory[i].itemName == "bottle")
                {
                    GameObject temp =
                        Instantiate(blankInventorySlot, inventoryPanelContent.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanelContent.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory2.myInventory[i], this);
                    }
                }

            }
            //
            //
            //
            /*
            for (int i = 0; i < saveIDList.Count; i++)
            {
                if (saveIDList[i] == itemDatabase.itemDB[i].itemID)
                {
                    inventory2.inventoryContainer.Add(itemDatabase.itemDB[i]);
                    //if the loaded IDs are matched with itemDatabase's items (which holds all your items so obviously they have itemID), then
                    //Load the exact item to the inventory. 
                }
            }
            */
            //
            //
            //
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        Debug.Log("made inventory");
        SetTextAndButton("", false);
    }

    public void SetupDescriptionAndButton(string newDescriptionString,
        bool isButtonUsable, Inventory2Item newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        useButton.SetActive(isButtonUsable);
    }

    void ClearInventorySlots()
    {
        for(int i = 0; i < inventoryPanelContent.transform.childCount; i ++)
        {
            Destroy(inventoryPanelContent.transform.GetChild(i).gameObject);
        }
    }
    public void UseButtonPressed()
    {
        if(currentItem)
        {
            currentItem.Use();
            //clear all of the inventory slots
            ClearInventorySlots();
            //refill all of the slots to update them
            MakeInventorySlots();
            if(currentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
            }
        }
    }
}
