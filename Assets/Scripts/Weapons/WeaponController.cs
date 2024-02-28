using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    private WeaponAnimator _weaponAnimator;
    private StateMachine _stateMachine;
    [field: SerializeField] public Weapon Weapon { get; private set; }
    

    [HideInInspector] public float ÑhargeModifier;
    [HideInInspector] public int AttackButton = -1;
    [HideInInspector] public int BlockButton = -1;
    public int AttackNumber { get; private set; }

    public bool HasWeapon { get { return Weapon != null; } }

    private void Awake()
    {
        _weaponAnimator = GetComponentInChildren<WeaponAnimator>();

    }

    private void Start()
    {
        AttackNumber = 1;

        SetUpStateMachine();
    }

    private void SetUpStateMachine()
    {
        _stateMachine = new StateMachine();

        var idleState = new WeaponIdle(this, _weaponAnimator.Animator, _stateMachine);
        var chargeState = new WeaponCharge(this, _weaponAnimator.Animator, _stateMachine);
        var attackState = new WeaponAttack(this, _weaponAnimator.Animator, _stateMachine);
        var recoveryState = new WeaponRecovery(this, _weaponAnimator.Animator, _stateMachine);
        var blockState = new WeaponBlock(this, _weaponAnimator.Animator, _stateMachine);

        _stateMachine.AddTransition(idleState, chargeState, () => GetMouseButton(AttackButton));
        _stateMachine.AddTransition(chargeState, attackState, () => GetMouseButtonUp(AttackButton));

        _stateMachine.AddTransition(idleState, blockState, () => GetMouseButton(BlockButton));
        _stateMachine.AddTransition(recoveryState, blockState, () => GetMouseButton(BlockButton));
        _stateMachine.AddTransition(blockState, idleState, () => !GetMouseButton(BlockButton) && blockState.Completed);

        _stateMachine.AddTransition(attackState, recoveryState, () => attackState.Completed);
        _stateMachine.AddTransition(recoveryState, idleState, () => recoveryState.Completed);
        _stateMachine.AddTransition(recoveryState, chargeState, () =>  
        {
            if (AttackNumber != 3 && GetMouseButton(AttackButton)) 
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

        if(Weapon.Type==WeaponType.OffHand)UIDebug.Instance.Show("Weapon: ", _stateMachine.CurrentStateName.Replace("Weapon", ""), "yellow");
        //UIDebug.Instance.Show("Charge: ", ÑhargeModifier.ToString(), "purple");
    }

    private void NextAttack()
    {
        AttackNumber++;
        if (AttackNumber > 3) AttackNumber = 1;
    }
    public void ResetAttackNumber()
    {
        AttackNumber = 1;
    }

    public void SetWeapon(Weapon weapon)
    {
        if (weapon == null) { enabled = false; return; }
        else enabled = true;

        if (Weapon != null) Destroy(Weapon.gameObject);

        Weapon = Instantiate(weapon,_weaponAnimator.transform);
        Weapon.EnableCollider(false);

        _weaponAnimator.Animator.runtimeAnimatorController = Weapon.Animator;
        _weaponAnimator.OnEnable = () => Weapon.EnableCollider(true);
        _weaponAnimator.OnDisable = () => Weapon.EnableCollider(false);
    }

    private bool GetMouseButton(int button)
    {
        if (button == -1) return false;

        return Input.GetMouseButton(button);
    }
    private bool GetMouseButtonUp(int button)
    {
        if (button == -1) return false;

        return Input.GetMouseButtonUp(button);
    }
}
