using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoneyText : MonoBehaviour
{
    [SerializeField] private Text text;

    private void OnEnable()
    {
        text = GetComponent<Text>();
        
    }

    private void Start()
    {
        text.text = PlayerPrefs.GetInt("PlayerMoney").ToString();
    }

    private void Update()
    {
        text.text = PlayerPrefs.GetInt("PlayerMoney").ToString();
    }
}
