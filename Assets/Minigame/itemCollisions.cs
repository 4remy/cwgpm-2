using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class itemCollisions : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public float dirX;



    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       

    }

    // Update is called once per frame
    void Update()
    {
        //how do i make it go the opposite direction?
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Pizza>())
        {
            //rb.velocity = Vector2.zero;
            moveSpeed = 0f;



        }

        if (collision.GetComponent<Wall>())
        {
            Debug.Log("ingredient destroyed");
            Destroy(this.gameObject);

        }

    }

}