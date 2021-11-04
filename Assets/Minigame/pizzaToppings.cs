using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Pizza Topping", menuName = "Minigame/PizzaToppings")]
[System.Serializable]

public class pizzaToppings : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int points;
   // public bool usable;
   // public UnityEvent thisEvent;

    /*
    public void Use()
    {

        Debug.Log("Using " + itemName);
        thisEvent.Invoke();

    }

    public void DecreaseAmount(int amountToDecrease)
    {
        numberHeld -= amountToDecrease;
        if (numberHeld < 0)
        {
            numberHeld = 0;
        }
    }
    */
}
