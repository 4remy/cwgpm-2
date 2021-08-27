using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giveable : MonoBehaviour
{
    public bool GiveableCollided;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Giveable"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("player in giving range");
                GiveableCollided = true;
            }
        }

    }
}
