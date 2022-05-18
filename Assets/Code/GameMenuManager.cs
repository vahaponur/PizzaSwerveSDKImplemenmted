using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
   
   [SerializeField] private GameObject tapToPlay;
   [SerializeField] private PlayerController PlayerController;


   private void Start()
   {
       PlayerController = FindObjectOfType<PlayerController>();

   }

   private void Update()
   {
      
    
   }

   public void ActivateGame()
   {
      
         tapToPlay.SetActive(false);

         PlayerController.gameActive = true;
      
   }
}
