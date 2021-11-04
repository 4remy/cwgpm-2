﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toppingFall : MonoBehaviour
{
    private Rigidbody2D rb;
    public int setIngredientValue;
    static public int IngredientValue = 20;

    void Awake()
    {
        IngredientValue = setIngredientValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("egg clicked 1");
            //transform.parent = null;
            //rb.bodyType = RigidbodyType2D.Dynamic;
            //rb.WakeUp();

            rb = GetComponent<Rigidbody2D>();
            //rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

        }

        
    }

}