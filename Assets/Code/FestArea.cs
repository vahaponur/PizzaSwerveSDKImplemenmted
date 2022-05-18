using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FestArea : MonoBehaviour
{
    [field: SerializeField] public Transform[] Masalar { get; private set; }
    [field: SerializeField] public Animator[] Animators { get; private set; }
    [field:SerializeField] public Transform CamFinishPos { get; private set; }
    [field:SerializeField] public ParticleSystem[] Confetties { get; private set; }
    [field:SerializeField] public Transform[] Multipliers { get; private set; }
}
