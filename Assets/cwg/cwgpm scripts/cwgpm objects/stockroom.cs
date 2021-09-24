using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stockroom : Interactable
{
    [SerializeField] private Schwer.ItemSystem.Item item = default;
    public int increaseBy;
    public string soundEffectToPlay;


    protected override void Interact()
    {
        if (player.inventory[item] == 0)
        {
            Debug.Log("damn u ran out of the item");
            Debug.Log("i give");
            player.inventory[item] += increaseBy;
            AudioManager.Instance.Play(soundEffectToPlay);
            StartCoroutine(itemCo());

        }
        else
        {

            Debug.Log("already have some");
        }
    }



    IEnumerator itemCo()
    {
        player.RaiseItem(item);
        yield return new WaitForSeconds(0.5f);
         player.RaiseItem(null);

    }
}
