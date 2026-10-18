using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using System;
using Unity.VisualScripting;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;

    public TMP_Text coinText;
    public int _currentcoins = 0;
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        coinText.text = "COINS: " + _currentcoins.ToString();
    }

    public void IncreaseCoins(int v)
    {
        _currentcoins = v;
        coinText.text = "COINS: " + _currentcoins.ToString();
    }
}
