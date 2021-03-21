using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType
{
    random,
    whenClicked,
    specific
}

public class spriteChanger : MonoBehaviour
{
    public BehaviourType thisBehaviourType;

    public SpriteRenderer spriteRenderer;
    //public Sprite newSprite;
    public Sprite[] spriteArray;
    private bool spriteChanged = false;
    public Sprite clickedSprite;
    public Sprite specificSprite;
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
        if (thisBehaviourType == BehaviourType.random)
        {
            if (!spriteChanged)
            {
                RandomChange();
            }
        }

        if (thisBehaviourType == BehaviourType.whenClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!spriteChanged)
                {
                    ClickedSprite();
                }
            }
        }

        if (thisBehaviourType == BehaviourType.specific)
        {
            //the lack of sprite changed means this can happen a lot
            SetSpecific();
        }
    }
    void RandomChange()
    {
        spriteRenderer.sprite = spriteArray[UnityEngine.Random.Range(0, spriteArray.Length)];
        spriteChanged = true;
    }

    void ClickedSprite()
    {
        //changes to 'clicked sprite'
        spriteRenderer.sprite = clickedSprite;
        //locks after doing this once
        spriteChanged = true;
        //update method will look for if spriteChanged = true before doing anything 
    }

    void SetSpecific()
    {
        spriteRenderer.sprite = specificSprite;
    }
}
