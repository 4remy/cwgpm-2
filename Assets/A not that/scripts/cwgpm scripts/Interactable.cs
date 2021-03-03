using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool playerInRange;
    protected abstract void Interact();
    protected virtual void Exit() { }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            Interact();
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
            Exit();
        }
    }
}