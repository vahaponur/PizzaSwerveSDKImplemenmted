using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// For testing purpose to see fps on mobile
/// </summary>
public class FpsShow : MonoBehaviour
{
    #region Serialized Fields
    #endregion

    #region Private Fields

    private Text text;
    #endregion

    #region Public Properties
    #endregion

    #region MonoBehaveMethods
    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start()
    {
        StartCoroutine(ShowFps());
    }

   
    void Update()
    {
        
    }

    IEnumerator ShowFps()
    {
        while (true)
        {
            var fps =(int) (1.0f / Time.smoothDeltaTime);
            text.text ="FPS: "+ fps;
            yield return new WaitForSeconds(0.3f);
        }
    }
    #endregion
    
    #region PublicMethods
    #endregion
    
    #region PrivateMethods
    #endregion
}
