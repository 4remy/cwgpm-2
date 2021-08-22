using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SchwerInventory = Schwer.ItemSystem.Inventory;

public class ItemChanger : Interactable
{
    [Header("Inventory")]
    [SerializeField] private Schwer.ItemSystem.InventorySO _inventory = default;
    public SchwerInventory inventory => _inventory.value;

    //[Header("item to be changed")]
    [SerializeField] private Schwer.ItemSystem.Item oldItem = default;

    //[Header("New item")]
    [SerializeField] private Schwer.ItemSystem.Item newItem = default;



    //public BoxCollider2D box;


    // Start is called before the first frame update
    void Start()
    {
        //get animators here
    }


    protected override void Interact()
    {
        if (player.inventory[oldItem] > 0)
        {

            Debug.Log("you had the item I was looking for!");
            player.inventory[oldItem]--;
            player.inventory[newItem]++;
            //put animation here
        }
        else
        {
            Debug.Log("item not found");
        }
    }
}
