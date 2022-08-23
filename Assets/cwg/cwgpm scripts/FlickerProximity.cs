using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerProximity : MonoBehaviour
{
    // this is incredibly janky and should only be used in Cobyland
    // it actually works very well unintentionally
    public bool playerInRange;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public int currentSprite;
    private bool changeTriggered;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if(changeTriggered)
            {
                return; 
            }
            else
            {
                StartCoroutine(ChangeCo());
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }



    private IEnumerator ChangeCo()
    {
        changeTriggered = true;
        spriteRenderer.sprite = spriteArray[currentSprite];
        currentSprite++;

        if (currentSprite >= spriteArray.Length)
        {
            currentSprite = 0;
        }
        yield return new WaitForSeconds(5f);
        changeTriggered = false;
    }

}