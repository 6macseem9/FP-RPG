using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public enum WeaponType { Sword }

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponAnimator _weaponAnimator;
    [field: SerializeField] public TestWeapon Weapon { get; private set; }
    private StateMachine _stateMachine;

    [HideInInspector] public float ÑhargeModifier;

    public string WeaponName { get { return Weapon.WeaponType.ToString(); } }
    public int _attackNumber { get; private set; }
    public string AttackString { get { return WeaponName + _attackNumber; } }
    

    void Start()
    {
        SetUpStateMachine();

        _weaponAnimator.OnEnable += () => Weapon.EnableCollider(true);
        _weaponAnimator.OnDisable += () => Weapon.EnableCollider(false);

        _attackNumber = 1;
    }

    private void SetUpStateMachine()
    {
        _stateMachine = new StateMachine();

        var idleState = new WeaponIdle(this, _weaponAnimator.Animator, _stateMachine);
        var chargeState = new WeaponCharge(this, _weaponAnimator.Animator, _stateMachine);
        var attackState = new WeaponAttack(this, _weaponAnimator.Animator, _stateMachine);
        var recoveryState = new WeaponRecovery(this, _weaponAnimator.Animator, _stateMachine);

        _stateMachine.AddTransition(idleState, chargeState, () => Input.GetMouseButton(0));
        _stateMachine.AddTransition(chargeState, attackState, () => Input.GetMouseButtonUp(0));
        _stateMachine.AddTransition(attackState, recoveryState, () => attackState.Completed);
        _stateMachine.AddTransition(recoveryState, idleState, () => recoveryState.Completed);
        _stateMachine.AddTransition(recoveryState, chargeState, () =>  
        {
            if (_attackNumber != 3 && Input.GetMouseButton(0)) 
            { 
                NextAttack(); 
                return true; 
            }
            return false;
        });

        _stateMachine.SetState(idleState);
    }
    
    void Update()
    {
        _stateMachine.Update();

        UIDebug.Instance.Show("Weapon: ", _stateMachine.CurrentStateName.Replace("Weapon", ""), "yellow");
        UIDebug.Instance.Show("Charge: ", ÑhargeModifier.ToString(), "purple");
    }

    private void NextAttack()
    {
        _attackNumber++;
        if (_attackNumber > 3) _attackNumber = 1;
    }
    public void ResetAttackNumber()
    {
        _attackNumber = 1;
    }
}
