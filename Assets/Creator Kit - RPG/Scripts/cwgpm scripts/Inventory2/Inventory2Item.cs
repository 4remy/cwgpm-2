using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory2/Items")]
public class Inventory2Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public Sprite itemImageClose;
    public int numberHeld;
    public bool usable;
    public bool unique;
}
