using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    public Animator Animator { get; private set; }

    public Action OnEnable;
    public Action OnDisable;

    void Awake()
    {
        Animator = GetComponent<Animator>();    
    }

    public void EnableCollider()
    {
        OnEnable?.Invoke();
    }

    public void DisableCollider()
    {
        OnDisable?.Invoke();
    }
}
