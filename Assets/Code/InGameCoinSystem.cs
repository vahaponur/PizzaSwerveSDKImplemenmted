using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCoinSystem : MonoBehaviour
{
   public int TotalCoin { get;private set; }

   private void Start()
   {
      
   }

   public void AddOneCoin()
   {
      TotalCoin++;
   }

   public IEnumerator AddCoinWithTime(int amount, float timeBetween)
   {
      for (int i = 0; i < amount; i++)
      {
         AddOneCoin();
         yield return new WaitForSeconds(timeBetween);
      }
   }
   
}
