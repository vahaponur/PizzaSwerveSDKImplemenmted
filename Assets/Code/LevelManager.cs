using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private void Update()
    {
        if (levelText)
        {
            levelText.text = "Level  " + PlayerPrefs.GetInt("Level");
        }
    }

    public void Reload()
    {
        DOTween.KillAll();
        DOTween.Clear(true);
        SceneManagerAdapter.ReloadLevel();
        DOTween.KillAll();
        DOTween.Clear(true);
    }

    public void NextLevel()
    {
        DOTween.KillAll();
        DOTween.Clear(true);
        PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level")+1);
       
        
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
       
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        PlayerPrefs.SetInt("LastLevelIndex",nextSceneIndex);
        SceneManagerAdapter.LoadNextLevel();
        DOTween.KillAll();
        DOTween.Clear(true);
    }
}
