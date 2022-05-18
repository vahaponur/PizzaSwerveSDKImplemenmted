using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuLayout;
    [SerializeField] private GameObject FailMenuLayout;
    [SerializeField] private GameObject SuccessMenuLayout;
    public bool gameSuccessFinish, gameFailFinish;

    private void Update()
    {
        if (gameFailFinish)
        {
            MainMenuLayout.SetActive(false);
            FailMenuLayout.SetActive(true);
        }

        if (gameSuccessFinish)
        {
            MainMenuLayout.SetActive(false);
            SuccessMenuLayout.SetActive(true);
        }
    }
}
