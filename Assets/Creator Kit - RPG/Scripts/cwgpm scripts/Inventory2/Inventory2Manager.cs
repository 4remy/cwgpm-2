using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory2Manager : MonoBehaviour
{
    [Header("Inventory Information")]
    public PlayerInventory2 playerInventory2;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanelContent;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public Inventory2Item currentItem;

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
        {
            for(int i = 0; i < playerInventory2.myInventory.Count; i++)
            {
                GameObject temp =
                    Instantiate(blankInventorySlot, inventoryPanelContent.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanelContent.transform);
                InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                if(newSlot)
                {
                    newSlot.Setup(playerInventory2.myInventory[i], this);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

    public void SetupDescriptionAndButton(string newDescriptionString,
        bool isButtonUsable, Inventory2Item newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        useButton.SetActive(isButtonUsable);
    }

    public void UseButtonPressed()
    {
        if(currentItem)
        {
            currentItem.Use();
        }
    }
}
