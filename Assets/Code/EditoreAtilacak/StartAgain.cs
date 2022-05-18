using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// EditorOnlyScript to reload current level
/// </summary>
public class StartAgain : MonoBehaviour
{
   private bool patlamadi = true;
   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("Player") && patlamadi)
      {
         DOTween.Clear(true);
         patlamadi = false;
         StartCoroutine(LoadYourAsyncScene());
      }
   }
   IEnumerator LoadYourAsyncScene()
   {
      // The Application loads the Scene in the background as the current Scene runs.
      // This is particularly good for creating loading screens.
      // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
      // a sceneBuildIndex of 1 as shown in Build Settings.

      AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

      // Wait until the asynchronous scene fully loads
      while (!asyncLoad.isDone)
      {
         DOTween.Clear(true);
         yield return null;
      }
   }
}
