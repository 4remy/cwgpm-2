using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomChange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    //public Sprite newSprite;
    public Sprite[] spriteArray;
    private bool spriteChanged = false;
    /*void Update()
    {
         change sprite when clicked
        if (Input.GetMouseButtonDown(0))
        {
            ChangeSprite(newSprite);
        }
        
    }*/
    void Update()
    {
        if (!spriteChanged)
        {
            ChangeSprite();
        }
       
    }
    void ChangeSprite()
    {
        spriteRenderer.sprite = spriteArray[UnityEngine.Random.Range(0, spriteArray.Length)];
        spriteChanged = true;
    }
}
