using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TekerRot : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        transform.
            Rotate(Vector3.right*speed*Time.deltaTime,Space.Self);
    }
}
