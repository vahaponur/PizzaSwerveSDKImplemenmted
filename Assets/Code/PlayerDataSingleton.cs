using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Includes data belonging the player (State, Curren Points etc.)
/// </summary>
public sealed class PlayerDataSingleton : MonoBehaviour
{
    /// <summary>
    /// Singleton all along the game
    /// </summary>
    public static PlayerDataSingleton Instance { get; private set; }

    private void Awake()
    {
    
        SetPlayerPrefs();
        ProduceSingleInstance();
    }

    private void Start()
    {
        StartCoroutine(SceneManagerAdapter.LoadSceneAsync(PlayerPrefs.GetInt("LastLevelIndex")));

    }

    void ProduceSingleInstance()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void SetPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("PlayerMoney"))
            PlayerPrefs.SetInt("PlayerMoney", 0);


        if (!PlayerPrefs.HasKey("Level"))
            PlayerPrefs.SetInt("Level", 1);
        if (!PlayerPrefs.HasKey("Voice"))
        {
            PlayerPrefs.SetInt("Voice",1);
        }
        if (!PlayerPrefs.HasKey("Vibration"))
        {
            PlayerPrefs.SetInt("Vibration",1);
        }

        if (!PlayerPrefs.HasKey("LastLevelIndex"))
        {
            PlayerPrefs.SetInt("LastLevelIndex",SceneManager.GetActiveScene().buildIndex);
        }
    }
}