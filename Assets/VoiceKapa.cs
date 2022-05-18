using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceKapa : MonoBehaviour
{
    [SerializeField] private Sprite active;
    [SerializeField] private Sprite passive;
    private Image Image;

    private void Start()
    {
        Image = GetComponent<Image>();
        Image.sprite = active;
    }

    public void OpenCloseSound()
    {
        if (PlayerPrefs.GetInt("Voice")==1)
        {
            PlayerPrefs.SetInt("Voice",0);
            Debug.Log("dfsd");
            Image.sprite = passive;
        }

        else if (PlayerPrefs.GetInt("Voice")==0)
        {
            PlayerPrefs.SetInt("Voice",1);
            Image.sprite = active;
        }
        
    }


}
