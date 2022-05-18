using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FOVAyarla : MonoBehaviour
{
   private CinemachineVirtualCamera VirtualCamera;
   private StackHandler StackHandler;

   private CinemachineTransposer transposer;
   [SerializeField] private float startFov;
   [SerializeField] private float givenZ;
   private void Awake()
   {
      VirtualCamera = GetComponent<CinemachineVirtualCamera>();
     transposer  = VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
     startFov =transposer.m_FollowOffset.z ;
     givenZ = startFov;
 
   }

   private void Start()
   {

     
      StackHandler = FindObjectOfType<StackHandler>();
     
   }

   private void LateUpdate()
   {
      var nextFov = startFov - StackHandler.StackCount / 100f;

      transposer.m_FollowOffset.z = Mathf.Lerp(transposer.m_FollowOffset.z,nextFov,Time.deltaTime*2);
#if UNITY_EDITOR
      if (!UnityEditor.EditorApplication.isPlaying)
      {
         transposer.m_FollowOffset.z = givenZ;
      }
#endif

   }

   private void OnDisable()
   {
      transposer.m_FollowOffset.z = givenZ;
   }

   private void OnApplicationQuit()
   {
      transposer.m_FollowOffset.z = givenZ;
   }

   private void OnApplicationPause(bool pauseStatus)
   {
      transposer.m_FollowOffset.z = givenZ;

   }
   
}
