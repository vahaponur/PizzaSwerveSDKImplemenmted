using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class StackHandler : MonoBehaviour
{
    private List<Transform> pizzaStack = new List<Transform>();
    [SerializeField] private Transform leftStack;
    [SerializeField] private Transform rightStack;
    [SerializeField] private GameObject pizzaL;
    [SerializeField] private GameObject pizzaR;
    [SerializeField] private Camera mainCam;

    private HitEffectPool hitEffectPool;
    private Transform[] festAreaTables;
    private Transform[] serveAreaTables;
    private int serveAmount;
    private Animator[] animators;
    private bool tableServed;
    private int numOnLeft;
    private int numOnRight;
    private bool onFinish;
    [SerializeField] private TextMeshPro stackCountText;

    [SerializeField] PlayerController m_PlayerController;

    private MathOperation opToTake;
    private bool mathInfoTaken;
    public int StackCount => pizzaStack.Count;
    private float speedHolder;
    private FestArea FestArea;
    private int finishPizzaCount;
    private bool finishMoveHandled = false;
    private InGameCoinSystem InGameCoinSystem;
    private GameManager m_GameManager;
    private float swerveSpeedHolder;
    private int lastFov;
    [SerializeField] AudioClip dagitimSesi;
    [SerializeField] private AudioClip toplamaSesi;
    private AudioSource audioSource;
    [SerializeField] private AudioClip animSesi;
    [SerializeField] private AudioClip finishSesi;
    [SerializeField] private AudioClip azalisSesi;
    private bool finishPlayed = false;
    private ServeArea serveArea;
    private void Awake()
    {
  
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InGameCoinSystem = GameObject.FindObjectOfType<InGameCoinSystem>();
        m_GameManager = FindObjectOfType<GameManager>();
        hitEffectPool = GetComponent<HitEffectPool>();
        HandleInstance(pizzaR, rightStack, new Vector3(0,0.02f * (rightStack.transform.childCount),0));
        HandleInstance(pizzaL, leftStack, new Vector3(0,0.02f * (leftStack.transform.childCount),0));
        speedHolder = m_PlayerController.speed;
        swerveSpeedHolder = m_PlayerController.swerveSpeed;
        lastFov = (int)mainCam.fieldOfView + 5;
    }

    private void Update()
    {
        audioSource.volume = PlayerPrefs.GetInt("Voice") * 0.8f;
        stackCountText.text = pizzaStack.Count.ToString();
        //If there is ano input fix the pizza boxes rotations
        if (!m_PlayerController.onTurn)
        {
            pizzaStack.ForEach(e => e.rotation = Quaternion.identity);
        }

        if (onFinish)
        {
           
            finishPizzaCount = pizzaStack.Count;
            
            mainCam.GetComponent<CinemachineBrain>().enabled = false;
            mainCam.transform.position = Vector3.Slerp(mainCam.transform.position, FestArea.CamFinishPos.position,
                    Time.deltaTime * 3);
            mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation,FestArea.CamFinishPos.rotation,Time.deltaTime*2);
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, lastFov, Time.deltaTime * 3);
            if (!finishMoveHandled)
            {
                
                PlayerPrefs.SetInt("PlayerMoney",PlayerPrefs.GetInt("PlayerMoney")+finishPizzaCount+InGameCoinSystem.TotalCoin);
                StartCoroutine(MoveToMultiplier(finishPizzaCount));
                finishMoveHandled = true;
            }
      

        }
        if (pizzaStack.Count == 0)
        {
           
            if (!onFinish)
            {
             
                m_PlayerController.speed = 0;
                m_PlayerController.swerveSpeed = 0f;
                m_PlayerController.motor.transform.DORotate(new Vector3(0, 0, 85), 3f);
                m_GameManager.gameFailFinish = true;
            }

            if (onFinish)
            {
                
                foreach (ParticleSystem festAreaConfetty in FestArea.Confetties)
                {
                    festAreaConfetty.PlayWithLogic();
                }

          
            }
          
        }
    }

    IEnumerator MoveToMultiplier(int finishCountBeforeServe)
    {
        var finishPosIndex = (int)(finishCountBeforeServe * 0.1f);
        finishPosIndex = Mathf.Clamp(finishPosIndex, 0, 9);
        Vector3 posToGo = new Vector3(m_PlayerController.transform.position.x,
            m_PlayerController.transform.position.y, FestArea.Multipliers[finishPosIndex].position.z);

        while (Mathf.Abs(m_PlayerController.transform.position.z - posToGo.z)>0.05f)
        {
            m_PlayerController.gameObject.transform.position =
                Vector3.Lerp(m_PlayerController.gameObject.transform.position, posToGo, Time.deltaTime * 1f);
            yield return new WaitForEndOfFrame();
        }

        if (Mathf.Abs(m_PlayerController.transform.position.z - posToGo.z)<=0.1f)
        {
            transform.DORotate(new Vector3(0, transform.eulerAngles.y - 180, 0),0.25f).OnComplete(() =>
            {
                m_GameManager.gameSuccessFinish = true;
                if (!finishPlayed)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(finishSesi);
                    finishPlayed = true;
                }
            });
            
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleNewcominPizzaTrigger(other);
        if (other.CompareTag("FestArea"))
        {
            FestArea = other.GetComponent<FestArea>();
            festAreaTables = FestArea.Masalar;
            animators = FestArea.Animators;
            animators.TriggerAll("motorenter");
            
        }

        if (other.CompareTag("ServeArea"))
        {
            serveArea = other.GetComponent<ServeArea>();
            serveAmount = serveArea.PizzaToServe;
            serveAreaTables = serveArea.tables;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MathOp") && !mathInfoTaken)
        {
            var mathInfo = other.GetComponent<MathChoise>();
            mathInfoTaken = true;
            var amountToTake = mathInfo.AmountToSend;
            opToTake = mathInfo.OpToSend;
            HandleMathOp(amountToTake, opToTake);
        }

        if (other.CompareTag("TweenerMathOp") && !mathInfoTaken)
        {
            var mathInfo = other.GetComponent<OneMath>();
            mathInfoTaken = true;
            var amountToTake = mathInfo.AmountToSend;
            opToTake = mathInfo.OpToSend;
            HandleMathOp(amountToTake, opToTake);
        }

        onFinish = other.CompareTag("FestArea");
        if (other.CompareTag("FestArea") && festAreaTables != null && !tableServed)
        {
            StartCoroutine(FestAreaHandout());

            tableServed = true;
        }
        if (other.CompareTag("ServeArea") && serveAreaTables != null && !tableServed)
        {
            StartCoroutine(ServeAreaHandout());
            tableServed = true;
        }
    }
    /// <summary>
    /// Called on fest area
    /// </summary>
    /// <returns></returns>
    IEnumerator FestAreaHandout()
    {
        m_PlayerController.cc.enabled = false;
        m_PlayerController.swerveSpeed = 0f;
        int loopCount = 0;
        var count = pizzaStack.Count;
        while (pizzaStack.Count>0)
        {
            for (int i = 0; i < festAreaTables.Length; i++)
            {
                if (pizzaStack.Count > 0)
                {
                    audioSource.PlayOneShotWithLogic(dagitimSesi);
                    Transform refp = pizzaStack[pizzaStack.Count - 1];
                    refp.SetParent(null);
                    pizzaStack.Remove(refp);
                    var tablePos = festAreaTables[i].position;
                    var pos = new Vector3(tablePos.x, tablePos.y + loopCount * 0.025f, tablePos.z);
                    refp.DOMove(pos, 0.1f);
                    yield return new WaitForSeconds(0.02f);
                }
            }

            loopCount++;
        }


        //StartCoroutine(IncreaseSpeed());
    }

    IEnumerator ServeAreaHandout()
    {
        m_PlayerController.swerveSpeed *= 0f;
        m_PlayerController.speed *=0f;
        int loopCount = 0;
        var count = pizzaStack.Count;
       
        while (serveAmount > 0)
        {
            serveArea.ChangeParticleColor();
            if ( pizzaStack.Count==0)
            {
                break;
                
            }
            for (int i = 0; i < serveAreaTables.Length; i++)
            {
                if (pizzaStack.Count > 0 && serveAmount > 0)
                {
                  
                    
                    audioSource.PlayOneShotWithLogic(dagitimSesi);
                    Transform refp = pizzaStack[pizzaStack.Count - 1];
                    refp.SetParent(null);
                    pizzaStack.Remove(refp);
                    var tablePos = serveAreaTables[i].position;
                    var pos = new Vector3(tablePos.x, tablePos.y + loopCount * 0.025f, tablePos.z);
                    refp.DOMove(pos, 0.1f);
                    serveAmount--;
                    yield return new WaitForSeconds(0.05f);
                }

                
            }

            
            loopCount++;
        }

        if (serveAmount <=0)
        {
            serveArea.PlayOkayParticle();
            m_PlayerController.swerveSpeed = swerveSpeedHolder;
            StartCoroutine(IncreaseSpeed());


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MathOp") || other.CompareTag("TweenerMathOp"))
        {
            mathInfoTaken = false;
        }

        if (tableServed)
        {
            tableServed = false;
        }
    }

    IEnumerator IncreaseSpeed()
    {
        while (m_PlayerController.speed < speedHolder)
        {
            m_PlayerController.speed += Time.deltaTime*1.5f;
            yield return new WaitForEndOfFrame();
        }
    }
    /// <summary>
    /// When player triggers a pizza on floor this method will be handling the sequence (pizza shrink, box instance etc.)
    /// </summary>
    /// <param name="other"></param>
    /// <param name="parent"></param>
    /// <exception cref="Exception"></exception>
    void StartCollectSequence(Collider other, Transform parent)
    {
        if (parent != rightStack &&
            parent != leftStack)
            throw new Exception("Wrong usage of this function");
        audioSource.PlayOneShotWithLogic(toplamaSesi);
        bool onRightStack = parent == rightStack;
        var yPos = onRightStack ? 0.02f * (rightStack.transform.childCount) : 0.02f * (leftStack.transform.childCount);
        var childPos = new Vector3(0, yPos, 0);
        var vectorToGo = parent.TransformPoint(childPos);
        Vibration.Vibrate(100);
        other.transform.DOMove(vectorToGo, 0.1f).OnComplete(() =>
        {
            other.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                var yPos = onRightStack
                    ? 0.02f * (rightStack.transform.childCount)
                    : 0.02f * (leftStack.transform.childCount);
                var childPos = new Vector3(0, yPos, 0);

                var instanceObject = onRightStack ? pizzaR : pizzaL;
                HandleInstance(instanceObject, parent, childPos);


                other.gameObject.SetActive(false);
                StartCoroutine(Anim());
            });
        });
    }

    /// <summary>
    /// Call this function whenever you need to instance a box
    /// </summary>
    /// <param name="go">which box type will be instantiated</param>
    /// <param name="parent">parent of instantiated box</param>
    /// <param name="position">last position to sit</param>
    void HandleInstance(GameObject go, Transform parent, Vector3 position)
    {
        var box = Instantiate(go);
        box.transform.SetParent(parent);
        box.transform.localPosition = position;
        box.transform.localScale = Vector3.one*1.2f;
        var boxPoz = box.transform.position;
        hitEffectPool.PlayAvaliableHit(boxPoz);
        hitEffectPool.PlayAvaliablePlusOne(new Vector3(boxPoz.x,boxPoz.y,boxPoz.z-0.1f));
        pizzaStack.Add(box.transform);
        InGameCoinSystem.AddOneCoin();
    }

    /// <summary>
    /// What happens when player trigger with a pizza on floor
    /// </summary>
    /// <param name="other">Pizza</param>
    void HandleNewcominPizzaTrigger(Collider other)
    {
        if (other.CompareTag("Stackable"))
        {
            var data = other.GetComponent<PizzaData>();
            if (data )
            {
                if (!data.Collected)
                {
                    var parent = rightStack.transform.childCount < leftStack.transform.childCount ? rightStack : leftStack;
                    StartCollectSequence(other, parent);
                    data.Collected = true;
                }

            }
          
        }
    }

    void AddPizzaToStack(int amount)
    {
        audioSource.PlayOneShot(animSesi);
        for (int i = 0; i < amount; i++)
        {
            if (numOnLeft <= numOnRight)
            {
                var yPos = 0.02f * (leftStack.transform.childCount);
                var childPos = new Vector3(0, yPos, 0);
                HandleInstance(pizzaL, leftStack, childPos);
                numOnLeft++;
            }
            else
            {
                var yPos = 0.02f * (rightStack.transform.childCount);
                var childPos = new Vector3(0, yPos, 0);
                HandleInstance(pizzaR, rightStack, childPos);
                numOnRight++;
            }
        }
    }

    void DeletePizzaFromStack(int amount)
    {
        audioSource.PlayOneShotWithLogic(azalisSesi);
        for (int i = 0; i < amount; i++)
        {
            if (i == 0)
            {
                if (pizzaStack.Count > 0)
                {
                    hitEffectPool.PlayAvaliableUzgun(new Vector3(leftStack.transform.position.x+0.1f,leftStack.transform.position.y+0.1f,leftStack.transform.position.z -0.1f));

                }
            }
            if (pizzaStack.Count < 1)
            {
                break;
            }

            Transform pizzaToGo = null;
            if (numOnLeft >= numOnRight)
            {
                pizzaToGo = pizzaStack[pizzaStack.Count - 1];
                pizzaStack.Remove(pizzaToGo);
                numOnLeft--;
            }
            else
            {
                pizzaToGo = pizzaStack[pizzaStack.Count - 1];
                pizzaStack.Remove(pizzaToGo);
                numOnRight--;
            }

            Destroy(pizzaToGo.gameObject);

        }
    }
    /// <summary>
    /// What happens when player triggers a mathematic operation
    /// </summary>
    /// <param name="amount">Amount on math operation</param>
    /// <param name="operation">Operation of math op</param>
    void HandleMathOp(int amount, MathOperation operation)
    {
        var count = pizzaStack.Count;
        var sonHal = 0;
        DOTween.Clear(true);
        if (operation == MathOperation.SUM)
        {
            sonHal += amount;
            AddPizzaToStack(sonHal);    
        }

        if (operation == MathOperation.DIVIDE)
        {
            sonHal = (count / amount);
            sonHal = count-sonHal;
            
            DeletePizzaFromStack(sonHal);
       
        }

        if (operation == MathOperation.MULTIPLY)
        {
            sonHal = (count * amount);
            sonHal -= count;
            AddPizzaToStack(sonHal);
            
        }

        if (operation == MathOperation.SUBTRACT)
        {
            sonHal += amount;
            DeletePizzaFromStack(sonHal);
        }

    

        DOTween.RestartAll();
        StartCoroutine(Anim());
    }


    /// <summary>
    /// Gives the stack an animation
    /// </summary>
    /// <returns>WaitForSeconds</returns>
    IEnumerator Anim()
    {
        
        for (int i = 0; i < pizzaStack.Count; i++)
        {
            int a = i;
            pizzaStack[a].transform.DOScale(1.4F, 0.08F).SetEase(Ease.OutCubic)
                .OnComplete(() => { pizzaStack[a].DOScale(1.2f, 0.1f); });
            if (i > 120)
            {
                break;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}