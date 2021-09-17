using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSpriteonTouch : MonoBehaviour

{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterController2D>();
        if (player != null)
        {
            return;
        }
        else
        {
            spriteRenderer.sprite = sprite2;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<CharacterController2D>();
        if (player != null)
        {
            return;
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }

    }



}
