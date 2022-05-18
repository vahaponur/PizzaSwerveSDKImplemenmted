using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class OneMath : MonoBehaviour
{

    [field: SerializeField] public MathOperation operation { get; private set; }
    [field:SerializeField] private int Amount { get;  set; }
    [SerializeField] private Color IncreaseColor = Color.blue;
    [SerializeField] private Color DecreaseColor = Color.red;

    [SerializeField] private MeshRenderer mesh;

    [SerializeField] private TextMeshPro text;
    public int AmountToSend { get; private set; }
    public MathOperation OpToSend { get; private set; }
    private bool infoSended;
    private float startX;

    private void Start()
    {
        SetMaterial(mesh);
        SetText(text);
        startX = transform.position.x *0.5f;

    }

    private void Update()
    {
        Vector3 nextPos = new Vector3(startX * Mathf.Sin(Time.time*3) + startX, transform.position.y,
            transform.position.z);
        transform.position = nextPos;
    }

    void SetMaterial(MeshRenderer mesh)
    {
        

        if (operation == MathOperation.DIVIDE || operation == MathOperation.SUBTRACT)
        {
            mesh.material.SetColor("_Color", DecreaseColor);
        }

        if (operation == MathOperation.SUM || operation == MathOperation.MULTIPLY)
        {
            mesh.material.SetColor("_Color", IncreaseColor);
        }
    }
    void SetText(TextMeshPro tmp)
    {
       
        switch (operation)
        {
            case MathOperation.SUM:
                tmp.text = "+" + Amount;
                break;
            case MathOperation.SUBTRACT:
                tmp.text = "-" + Amount;
                break;
            case MathOperation.DIVIDE:
                tmp.text = "÷" + Amount;
                break;
            case MathOperation.MULTIPLY:
                tmp.text = "x" + Amount;
                break;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!infoSended && other.CompareTag("PlayerMesh"))
        {
            AmountToSend = Amount;
            OpToSend = operation;
       
            infoSended = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerMesh"))
        {
            gameObject.SetActive(false);
        }
    }
}
