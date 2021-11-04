using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyedByWall : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;

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
           // Debug.Log("landed on pizza");
            //rb.velocity = Vector2.zero;
            moveSpeed = 0f;

        }

        if (collision.GetComponent<Wall>())
        {
            Debug.Log("egg destroyed");
            Destroy(this.gameObject);

        }

    }
      /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Pizza>())
        {
            Debug.Log("landed on pizza");
            rb.velocity = Vector2.zero;

        }
    }*/
}