using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEditorInternal;
using TMPro;
using System;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _coyoteTime;
    [SerializeField] private Ease _jumpEase1;
    [SerializeField] private Ease _jumpEase2;
    [SerializeField] private float _gravityForce = -1.5f;
    [SerializeField] private float _onSlopeDownForce = -0.5f;
    [SerializeField] private Transform _orientation;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private StateMachine _stateMachine;

    private bool _grounded;
    [HideInInspector] public float YVelocity;

    private float _maxSlopeAngle = 46;
    private RaycastHit _slopeHit;
    private Vector3 _moveDirection;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        SetupStateMachine();

        
    }

    private void SetupStateMachine()
    {
        _stateMachine = new StateMachine();

        var groundedState = new PlayerGrounded(this);
        var fallState = new PlayerFall(this, _gravityForce,_coyoteTime,_stateMachine);
        var jumpState = new PlayerJump(this,_gravityForce,_jumpForce,_jumpTime,_jumpEase1,_jumpEase2);

        _stateMachine.AddTransition(groundedState, fallState, () => !_grounded);
        _stateMachine.AddTransition(fallState, groundedState, () => _grounded);

        _stateMachine.AddTransition(groundedState, jumpState, () => Input.GetKey(KeyCode.Space));
        _stateMachine.AddTransition(fallState, jumpState, () => Input.GetKey(KeyCode.Space) && fallState.CoyoteTime);

        _stateMachine.AddTransition(jumpState, groundedState, () => jumpState.CanLand && _grounded);
        _stateMachine.AddTransition(jumpState, fallState, () => jumpState.Completed);

        _stateMachine.SetState(groundedState);
    }

    private void Update()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _collider.height * 0.5f + 0.3f, _groundLayer);

        _stateMachine.Update();

        UIDebug.Instance.Show("State:", _stateMachine.CurrentStateName.Replace("Player",""),"yellow");
        UIDebug.Instance.Show("Velocity:", $"{_rb.velocity} = {_rb.velocity.magnitude}", "orange");
        UIDebug.Instance.Show("Y:", YVelocity.ToString(), "red");
        UIDebug.Instance.Show("On Slope:", OnSlope().ToString(), "red");
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void Move()
    {
        var horInput = Input.GetAxisRaw("Horizontal");
        var verInput = Input.GetAxisRaw("Vertical");

        _moveDirection = _orientation.forward * verInput + _orientation.right * horInput;

        if (OnSlope() && _moveDirection!=Vector3.zero && _rb.velocity.y <= 0) YVelocity = _onSlopeDownForce;

        _moveDirection.y = YVelocity;
        _rb.velocity = _moveDirection * _moveSpeed;

        SpeedControl();
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        if (flatVel.magnitude > _moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }
    private bool OnSlope()
    {
        if(!_grounded || _stateMachine.CurrentStateName == nameof(PlayerJump)) return false;

        float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
        return angle < _maxSlopeAngle && angle != 0;
    }
}

public static class Util
{
    public static Tweener Delay(float time, TweenCallback func, bool realTime = false)
    {
        float timer = 0;
        Tweener tween = DOTween.To(() => timer, x => timer = x, time, time).SetUpdate(realTime);
        tween.onComplete = func;
        return tween;
    }
}

