﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class itemCollisions : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;

    public delegate void LandedDelegate();
    public event LandedDelegate LandedEvent;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 3f;

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Pizza>())
        {
            //rb.velocity = Vector2.zero;
            moveSpeed = 0f;

            if (LandedEvent != null)
            {
                LandedEvent();
            }


        }

        if (collision.GetComponent<Wall>())
        {
            Debug.Log("ingredient destroyed");
            Destroy(this.gameObject);

        }

    }

}