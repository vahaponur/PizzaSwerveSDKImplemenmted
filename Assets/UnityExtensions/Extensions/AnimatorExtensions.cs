using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorExtensions 
{
   public static void TriggerAll(this Animator[] animator, string triggerName)
   {
      for (int i = 0; i < animator.Length; i++)
      {
         animator[i].SetTrigger(triggerName);
      }
   }
   public static void SetBoolAll(this Animator[] animator, string triggerName,bool sit)
   {
      for (int i = 0; i < animator.Length; i++)
      {
         animator[i].SetBool(triggerName,sit);
      }
   }

   
}
