using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnNodeHandler : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField]private GameObject nextNode;
    #endregion

    #region Private Fields
    #endregion

    #region Public Properties

    public Vector3 nextNodePosition { get; private set; }
    #endregion

    #region MonoBehaveMethods
    void Awake()
    {
	
    }

    void Start()
    {
        nextNodePosition = nextNode.transform.forward;
    }

   
    void Update()
    {
        
    }
    #endregion
    
    #region PublicMethods
    #endregion
    
    #region PrivateMethods
    #endregion
}
