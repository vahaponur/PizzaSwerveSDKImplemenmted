using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    #region Serialized Fields
    #endregion

    #region Private Fields
    #endregion

    #region Public Properties
    #endregion

    public static DontDestroyOnLoad Instance { get; set; }
    #region MonoBehaveMethods
    void Awake()
    {
        GameAnalytics.Initialize();
    }
 void ProduceSingleInstance()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        ProduceSingleInstance();

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
