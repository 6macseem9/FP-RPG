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

    public void Pause()
    {
        Animator.Pause();
    }
}

public static class ExtensionAnimator
{
    public static void Pause(this Animator animator)
    {
        animator.SetFloat("Speed", 0);
    }
    public static void Unpause(this Animator animator)
    {
        animator.SetFloat("Speed", 1);
    }
}
