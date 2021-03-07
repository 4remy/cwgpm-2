using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Inventory2Item> itemDB = new List<Inventory2Item>(); //I would rather change this to an Array than List because we add items to this by hand.
}