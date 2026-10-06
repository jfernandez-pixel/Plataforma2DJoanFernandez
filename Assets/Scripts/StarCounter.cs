using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;

    public TMP_Text starText;
    public int _currentStars = 0;

    void Awake ()
    {
        instance = this;
    }

    void Start()
    {
        starText.text = "STARS 0 | " + _currentStars.ToString();
    }

    public void IncreaseCoins(int v)
    {
        _currentStars += v;
        starText.text = "STARS 0 | " + _currentStars.ToString();
    }
}

