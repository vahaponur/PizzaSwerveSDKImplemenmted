using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ServeArea : MonoBehaviour
{
    #region Serialized Fields
    [field:SerializeField] public int PizzaToServe { get; private set; }
    [SerializeField] private TextMeshPro text;
    [SerializeField] public Transform[] tables;
    [HideInInspector]public Material matToChange;
    [SerializeField] private MeshRenderer ServeAreaMesh;

    [SerializeField]
    private ParticleSystem fireVFX;

    private ParticleSystem.MainModule main;
    private float vfxSpeed;
    [SerializeField] private ParticleSystem okParticle;
    private void Start()
    {
        main = fireVFX.main;
        text.text = "Serve \n" + PizzaToServe;
        matToChange = ServeAreaMesh.material;
        vfxSpeed = PizzaToServe is 0 ? 1/(PizzaToServe*0.1f):1;
    }

    public void ChangeParticleColor()
    {
        
        main.startColor = Color.Lerp(main.startColor.color, Color.green, Time.deltaTime  *vfxSpeed*30);
    }

    public void PlayOkayParticle()
    {
        okParticle.PlayWithLogic();
    }

    #endregion

}
