using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using PathCreation.Examples;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serialized Fields


    [SerializeField] public GameObject motor;
  

    #endregion

    #region Private Fields
    public CharacterController cc;
    private PathFollower pathFollower;
    private TurnNodeHandler turnNodeHandler;
    private Vector3 fp, ep;
    private SwerveInputSystem swerveInputSystem;
    private Vector3 nextPose;
    public float speed = 1.5f;
    #endregion

    #region Public Properties
    public bool onTurn;
    public bool gameActive;
    #endregion

    #region MonoBehaveMethods

    private bool trapped;
    public float swerveSpeed;
    
    void Awake()
    {
        pathFollower = GetComponent<PathFollower>();
        cc = GetComponent<CharacterController>();
        pathFollower.enabled = false;
        swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    void Start()
    {
    }


    void Update()
    {
        cc.Move(transform.up * -1f);
        if (gameActive)
        {
            if (!trapped)
            {
                HandleMove();

            }
            HandleTurn();
            HandleSwerve();
        }
  
    }

    void HandleSwerve()
    {
        // float dif = 0;
        // if (Input.GetMouseButtonDown(0))
        // {
        //     fp = Input.mousePosition;
        // }
        //
        // else if (Input.GetMouseButton(0))
        // {
        //     ep = Input.mousePosition;
        //      dif = ep.x - fp.x;
        //      fp = Input.mousePosition;
        //      if (dif>0)
        //      {
        //          dif = 1;
        //      }
        //
        //      if (dif<0)
        //      {
        //          dif = -1;
        //      }
        //  
        //      var gidilecek = motor.transform.localPosition + dif * Vector3.right * Time.deltaTime*swerveSpeed;
        //
        //     if (!onTurn)
        //     {
        //         gidilecek.x = Mathf.Clamp(gidilecek.x, -0.48f, 0.48f);
        //
        //     }
        //     else
        //     {
        //         gidilecek.x = Mathf.Clamp(gidilecek.x, -0.20f, 0.20f);
        //
        //     }
        //     
        //     motor.transform.localPosition = gidilecek;
        //    
        //
        // }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     dif = 0;
        // }
        var dif = swerveInputSystem.MoveFactorX;
        if (dif>0)
        {
            dif = 1;
        }
                
        if (dif<0)
        {
            dif = -1;
        }
        Quaternion bb = Quaternion.Euler(new Vector3(motor.transform.localEulerAngles.x,motor.transform.localEulerAngles.y,dif*10));
        motor.transform.localRotation = Quaternion.Slerp(motor.transform.localRotation,bb,10*Time.deltaTime);
        float swerveAmount = Time.deltaTime * swerveSpeed * swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -0.1f, 0.1f);
        var sdfas = new Vector3(swerveAmount, 0, 0);


        motor.transform.localPosition += sdfas;
            motor.transform.localPosition = new Vector3(Mathf.Clamp(motor.transform.localPosition.x, -0.43f, 0.43f),
                motor.transform.localPosition.y, motor.transform.localPosition.z);







    }

    void HandleMove()
    {
        pathFollower.enabled = false;
        cc.Move(transform.forward * Time.deltaTime * speed);

        
    }

    void HandleTurn()
    {
        if (onTurn)
        {
            var que = Quaternion.LookRotation(nextPose);
            transform.rotation = Quaternion.Slerp(transform.rotation, que, Time.deltaTime * 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TurnNodeTurn(other);
        if (other.CompareTag("Finish"))
        {
            DOTween.Clear(true);

        }

        if (other.CompareTag("Trap"))
        {
            trapped = true;
            speed = 0;
            var rb = GetComponent<Rigidbody>();
            cc.Move(-transform.forward*50*Time.deltaTime );
            StartCoroutine(OpenCc());
        }
    }

    IEnumerator OpenCc()
    {
        yield return null;
        trapped = false;
        StartCoroutine(AccelBro());
    }

    IEnumerator AccelBro()
    {
        while (speed<2)
        {
            speed += Time.deltaTime ;

            yield return null;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TurnNode")&&onTurn && !turnNodeHandler)
        {
            onTurn = false;
        }
    }

    void TurnNodeTurn(Collider other)
    {
        if (other.CompareTag("TurnNode"))
        {
             turnNodeHandler = other.GetComponent<TurnNodeHandler>();
            if (turnNodeHandler)
            {
                onTurn = true;
                nextPose = turnNodeHandler.nextNodePosition;
            }

            if (!turnNodeHandler)
            {
                turnNodeHandler = null;
                var que = Quaternion.LookRotation(other.gameObject.transform.forward);
                transform.DORotateQuaternion(que, 0.25f);
                float needZ = other.transform.position.z;
                Vector3 goVec = new Vector3(transform.position.x, transform.position.y, needZ);

                transform.DOMoveZ(needZ, 1f);
            }
        }
    }

    IEnumerator ExitTurnFixZ()
    {
        yield return null;
        for (int i = 0; i < 10; i++)
        {
            
        }
    }
    

    #endregion

    #region PublicMethods

    #endregion

    #region PrivateMethods

    #endregion

  
}