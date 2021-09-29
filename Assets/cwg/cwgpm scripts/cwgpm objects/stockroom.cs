using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stockroom : Interactable
{
    [SerializeField] private Schwer.ItemSystem.Item item = default;
    public int increaseBy;
    public string soundEffectToPlay;

    public GameObject dialogBox;
    public Text dialogText;
    [Multiline]
   public string dialog;

    public bool textOn;
    public Signal ConvoStartSignal;
    public Signal ConvoFinishSignal;

    protected override void Interact()
    {
        if (!playerInRange)
        {
            return;
        }

        if (player.inventory[item] == 0 && playerInRange)
        {
            Debug.Log("damn u ran out of the item");
            Debug.Log("i give");
            player.inventory[item] += increaseBy;
            AudioManager.Instance.Play(soundEffectToPlay);
            StartCoroutine(itemCo());

        }
        else
        {
            if(!textOn)
            {
                ShowText();
            }
            else
            {
                TextAlreadyShown();
            }
        }
    }



    IEnumerator itemCo()
    {
        player.RaiseItem(item);
        yield return new WaitForSeconds(0.5f);
         player.RaiseItem(null);

    }

    private void ShowText()
    {
        ConvoStartSignal.Raise();
        Debug.Log("already have some");
        dialogBox.SetActive(true);
        // dialog text = contents text;
        dialogText.text = dialog;
        textOn = true;


    }

    private void TextAlreadyShown()
    {
        dialogBox.SetActive(false);
        textOn = false;
        ConvoFinishSignal.Raise();
    }

}
