using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BadIngredientType
{
    spider,
    fetus
}

public class BadTopping : MonoBehaviour
{
    private Rigidbody2D rb;
    public int setBadIngredientValue;
    static public int BadIngredientValue = 20;
    public bool canClick;

    public BadIngredientType setBadIngredientType;

    public static BadIngredientType thisBadIngredientType;



    // this needs to be like, a new thing each time it appears
    //so far it thinks this is the only topping ever - thats why score and ingredient types only register once

    void Awake()
    {
        BadIngredientValue = setBadIngredientValue;

        thisBadIngredientType = setBadIngredientType;
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