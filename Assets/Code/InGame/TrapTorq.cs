using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTorq : MonoBehaviour
{
    #region Serialized Fields
    #endregion

    #region Private Fields

    private Rigidbody rb;
    #endregion

    #region Public Properties
    #endregion

    #region MonoBehaveMethods
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (rb.angularVelocity.magnitude < 5)
        {
            rb.AddTorque(Vector3.up*10);

        }
    }

    #endregion
    
    #region PublicMethods
    #endregion
    
    #region PrivateMethods
    #endregion
}
