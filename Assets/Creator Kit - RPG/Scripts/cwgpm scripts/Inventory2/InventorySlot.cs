using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Stuff to change")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;
    // [SerializeField] private Image CloseImage;

    [Header("Variables from the item")]
    //public Sprite itemSprite;
    //    public Sprite closeSprite;
   // public int numberHeld;
    public string itemDescription;
    public Inventory2Item thisItem;
    public Inventory2Manager thisManager;

    public void Setup(Inventory2Item newItem, Inventory2Manager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if(thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
