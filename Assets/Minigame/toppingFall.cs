using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//change enum to 'type'
//enums are usually integers not pizza toppings
public enum IngredientType
{
    egg,
    tomato,
    basil,
    pepperoni,
    cheese,
    olives,
    bacon,
    onion,
    greenpepper,
    mushroom,
    shrimp,
    redpepper,
    flowers

}

public class toppingFall : MonoBehaviour
{
    private Rigidbody2D rb;
    public int setIngredientValue;
    static public int IngredientValue = 20;
    public bool canClick;

    public IngredientType setIngredientType;

    public static IngredientType thisIngredientType;

 

//box colliders need to ignore each other for foods
//only pay attention to pizza

 void Awake()
    {
        IngredientValue = setIngredientValue;

        thisIngredientType = setIngredientType;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //this is now handled by the click manager.
    // Update is called once per frame
    /*void Update()
    {
        if(!canClick)
        {return;}
        else
        {
               if (Input.GetMouseButtonDown(0))
                {
                //Debug.Log("ingredient clicked 1");

                rb = GetComponent<Rigidbody2D>();
                //rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

            }
        }


    }
    */

    //this is technically not needed either
    private void OnTriggerEnter2D(Collider2D other)
    {
        canClick = true;
       // Debug.Log("canClick is " + canClick);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canClick = false;
        //Debug.Log("canClick is " + canClick);

    }

}