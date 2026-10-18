using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using System;
using Unity.VisualScripting;

public class StarCounter : MonoBehaviour
{
    public static StarCounter instance;

    public TMP_Text starText;
    public int _currentStars = 0;
    [SerializeField]private int _maxStars = 0;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        starText.text = "STARS :" + _maxStars + "|" + _currentStars.ToString();
    }

    public void IncreaseStars(int v)
    {
        _currentStars += v;
        starText.text = "STARS :10|" + _currentStars.ToString();

        if (_currentStars >= _maxStars)
        {
            //Win();
        }
    }

    /*public void Win()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.ChangeScene("Win")
        }
    }*/
}
