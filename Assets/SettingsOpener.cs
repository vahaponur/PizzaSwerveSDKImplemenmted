using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOpener : MonoBehaviour
{
   [SerializeField] private GameObject settings;

   public void AcKapa()
   {
      if (settings.activeInHierarchy)
      {
         settings.SetActive(false);
      }

      if (!settings.activeInHierarchy)
      {
         settings.SetActive(true);
      }
   }
}
