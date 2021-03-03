using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory2", menuName = "Inventory2/Player Inventory2")]
public class PlayerInventory2 : ScriptableObject
{
    public List<Inventory2Item> myInventory = new List<Inventory2Item>();
}
