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

    public bool textOn;

    public GameObject dialogBox;
    public Text dialogText;
    [Multiline]
    public string dialog;

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
            if (!textOn)
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
        AudioManager.Instance.Play("Conveyor");
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Stop("Conveyor");
        player.RaiseItem(item);
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Play(soundEffectToPlay);
        player.RaiseItem(null);

    }

    private void ShowText()
    {
        AudioManager.Instance.Play(soundEffectToPlay2);
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
