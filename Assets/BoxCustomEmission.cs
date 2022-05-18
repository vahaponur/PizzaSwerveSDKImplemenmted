using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoxCustomEmission : MonoBehaviour
{
    private Color shadow;
    private Color highlight;
    private Material mat;
    private bool matUpdated = false;
    private float dTime;
    private float dTime2, animTime;
    private bool animFinished;

    private void Start()
    {
       
            mat = GetComponent<MeshRenderer>().material;
         
            StartCoroutine(AnimColor("_EmissionColor", 0.3f, 0));
        
            matUpdated = true;
        

        animTime += Time.deltaTime;
    }



    IEnumerator AnimColor(string property, float time, float val)
    {
        
        while (dTime < 0.5)
        {
            dTime += Time.deltaTime;
            if (dTime<0.25)
            {
                mat.SetColor(property, Color.Lerp(mat.GetColor(property),Color.white, Time.deltaTime*10 ));
 
            }

            if (dTime > 0.25)
            {
                mat.SetColor(property, Color.Lerp(mat.GetColor(property),Color.black, Time.deltaTime*10 ));
            }
            yield return null;
        }

    }
}