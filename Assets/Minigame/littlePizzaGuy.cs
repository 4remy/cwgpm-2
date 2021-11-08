using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class littlePizzaGuy : MonoBehaviour
{
    public GameObject littleGuy;
    public string soundEffectToPlay;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            littleGuy.SetActive(false);
            AudioManager.Instance.Play(soundEffectToPlay);
            //play the 'pizza' sound also
        }
    }
}
