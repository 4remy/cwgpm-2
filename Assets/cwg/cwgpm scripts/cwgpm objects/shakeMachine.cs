using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shakeMachine : Interactable
{
    [SerializeField] private Schwer.ItemSystem.Item item = default;
    public int increaseBy;
    public string soundEffectToPlay;
    public string soundEffectToPlay2;


    public Signal ConvoStartSignal;
    public Signal ConvoFinishSignal;

    protected override void Interact()
    {
        if (!playerInRange)
        {
            return;
        }

        if (player.inventory[item] < 5 && playerInRange)
        {
            Debug.Log("damn u ran out of the item");
            Debug.Log("i give");
            player.inventory[item] += increaseBy;

            StartCoroutine(itemCo());

        }
        else
        {
            AudioManager.Instance.Play(soundEffectToPlay2);
            return;
        }
    }



    IEnumerator itemCo()
    {
        AudioManager.Instance.Play("Conveyor");
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Stop("Conveyor");
        player.RaiseItem(item);
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Play(soundEffectToPlay);
        player.RaiseItem(null);

    }



}
