using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectPool : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField, Range(4, 10)] private int hitPoolSize;
    [SerializeField] private ParticleSystem plusOneVFX;
    [SerializeField, Range(4, 10)] private int plusOnePoolSize;
    #endregion

    #region Private Fields

    private Stack<ParticleSystem> hitVFXPool = new Stack<ParticleSystem>();
    private Stack<ParticleSystem> plusOneVFXPool = new Stack<ParticleSystem>();
    private Stack<ParticleSystem> uzgun = new Stack<ParticleSystem>();
    [SerializeField] private ParticleSystem uzgunVfx;

    #endregion

    #region Public Properties
    #endregion

    #region MonoBehaveMethods
    void Awake()
    {
        for (int i = 0; i < hitPoolSize; i++)
        {
            hitVFXPool.Push(Instantiate(hitVFX,transform));
        }

        foreach (var particleSystem in hitVFXPool)
        {
            particleSystem.transform.localScale = Vector3.one*0.06f;
        }
        hitVFXPool.CloseAll();
        for (int i = 0; i < plusOnePoolSize; i++)
        {
            plusOneVFXPool.Push(Instantiate(plusOneVFX,transform));
        }

        foreach (var particleSystem in plusOneVFXPool)
        {
            particleSystem.transform.localScale = Vector3.one*0.1f;
        }
        plusOneVFXPool.CloseAll();
        for (int i = 0; i < 10; i++)
        {
            uzgun.Push(Instantiate(uzgunVfx,transform));
        }

        foreach (var particleSystem in uzgun)
        {
            particleSystem.transform.localScale = Vector3.one*0.1f;
        }
        uzgun.CloseAll();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    #endregion
    
    #region PublicMethods

    public void PlayAvaliableHit(Vector3 position)
    {
        ParticleSystem vfx = null;
        foreach (var nextVFX in hitVFXPool)
        {
            if (!nextVFX.gameObject.activeSelf)
            {
                vfx = nextVFX;
                break;
            }
        }
        if (!vfx) return;
        
        vfx.gameObject.SetActive(true);
        vfx.transform.position = position;
        vfx.PlayWithLogic();
        StartCoroutine(DisableVFX(vfx, 0.2f));
    }
    public void PlayAvaliablePlusOne(Vector3 position)
    {
        ParticleSystem vfx = null;
        foreach (var nextVFX in plusOneVFXPool)
        {
            if (!nextVFX.gameObject.activeSelf)
            {
                vfx = nextVFX;
                break;
            }
        }
        if (!vfx) return;
        
        vfx.gameObject.SetActive(true);
        vfx.transform.position = position;
        vfx.PlayWithLogic();
        StartCoroutine(DisableVFX(vfx, 0.2f));
    }

    public void PlayAvaliableUzgun(Vector3 position)
    {
        ParticleSystem vfx = null;
        foreach (var nextVFX in uzgun)
        {
            if (!nextVFX.gameObject.activeSelf)
            {
                vfx = nextVFX;
                break;
            }
        }
        if (!vfx) return;
        
        vfx.gameObject.SetActive(true);
        vfx.transform.position = position;
        vfx.PlayWithLogic();
        StartCoroutine(DisableVFX(vfx, 0.5f)); 
    }
    #endregion
    
    #region PrivateMethods

    IEnumerator DisableVFX(ParticleSystem vfx,float seconds)
    {
        yield return new WaitForSeconds(seconds);
        vfx.gameObject.SetActive(false);
    }
    #endregion
}
