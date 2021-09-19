using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTextManager : MonoBehaviour
{
    public CoinCounter playerCoins;
    public Text coinDisplay;
    public void UpdateCoinCount()
    {
        coinDisplay.text = "" + playerCoins.coins;
    }
}
