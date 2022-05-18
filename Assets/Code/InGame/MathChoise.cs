using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MathOperation
{
    SUM,
    SUBTRACT,
    DIVIDE,
    MULTIPLY
}

public class MathChoise : MonoBehaviour
{
    [field: SerializeField] public MathOperation LeftOperation { get; private set; }
    [field: SerializeField] public int LeftAmount { get; private set; }
    [field: SerializeField] public MathOperation RightOperation { get; private set; }
    [field: SerializeField] public int RightAmount { get; private set; }
    
    public MathOperation OpToSend { get; private set; }
    [SerializeField] private Color IncreaseColor = Color.blue;
    [SerializeField] private Color DecreaseColor = Color.red;
    [SerializeField] private MeshRenderer leftMesh;
    [SerializeField] private MeshRenderer rightMesh;
    [SerializeField] private TextMeshPro leftText;
    [SerializeField] private TextMeshPro rightText;
    public int AmountToSend { get; private set; }
    private bool infoSended;
    private void Start()
    {
        SetMaterial(rightMesh);
        SetMaterial(leftMesh);
        SetText(leftText);
        SetText(rightText);
    }

    void SetMaterial(MeshRenderer mesh)
    {
        MathOperation op = mesh == leftMesh ? LeftOperation : RightOperation;

        if (op == MathOperation.DIVIDE || op == MathOperation.SUBTRACT)
        {
            mesh.material.SetColor("_Color", DecreaseColor);
        }

        if (op == MathOperation.SUM || op == MathOperation.MULTIPLY)
        {
            mesh.material.SetColor("_Color", IncreaseColor);
        }
    }

    void SetText(TextMeshPro tmp)
    {
        var isLeft = tmp == leftText;
        MathOperation op = isLeft ? LeftOperation : RightOperation;
        var amount = isLeft ? LeftAmount : RightAmount;
        switch (op)
        {
            case MathOperation.SUM:
                tmp.text = "+" + amount;
                break;
            case MathOperation.SUBTRACT:
                tmp.text = "-" + amount;
                break;
            case MathOperation.DIVIDE:
                tmp.text = "÷" + amount;
                break;
            case MathOperation.MULTIPLY:
                tmp.text = "x" + amount;
                break;
        }
    }

    int SideOfTrigger(Collider other)
    {
        var disToRight = Vector3.SqrMagnitude(rightMesh.transform.position - other.transform.position);
        var disToLeft = Vector3.SqrMagnitude(leftMesh.transform.position - other.transform.position);
        if (disToRight < disToLeft)
        {
            rightMesh.gameObject.SetActive(false);
            rightText.gameObject.SetActive(false);
            return RightAmount;
        }
        leftText.gameObject.SetActive(false);
        leftMesh.gameObject.SetActive(false);
        return LeftAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!infoSended && other.CompareTag("PlayerMesh"))
        {
            AmountToSend = SideOfTrigger(other);
            if (AmountToSend == RightAmount)
            {
                OpToSend = RightOperation;
            }
            else
            {
                OpToSend = LeftOperation;
            }

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