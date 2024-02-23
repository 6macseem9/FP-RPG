using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _drag;
    [SerializeField] private Transform _orientation;
    [Space(5)]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;

    [Space(10)]
    [Header("Ground Check")]
    [SerializeField] private float _height;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _maxSlopeAngle;


    private Rigidbody _rigidbody;

    private Vector3 _moveDirection;
    private bool _grounded;
    private float _horInput;
    private float _verInput;
    private bool _canJump = true;
    private RaycastHit _slopeHit;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        ReadInput();

        _grounded = Physics.Raycast(transform.position, Vector3.down, _height * 0.5f + 0.2f, _groundLayer);
        if(_grounded) _rigidbody.drag = _drag;
        else _rigidbody.drag = _airDrag;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadInput()
    {
        _horInput = Input.GetAxisRaw("Horizontal");
        _verInput = Input.GetAxisRaw("Vertical");

        
    }

    private void Move()
    {
        _moveDirection = _orientation.forward * _verInput + _orientation.right * _horInput;
        
        Vector3 direction = _moveDirection;
        if (OnSlope())
        {
            direction = GetSlopeMoveDirection() * 2;
            if (_rigidbody.velocity.y > 0) _rigidbody.AddForce(Vector3.down * 80);
        }
        _rigidbody.useGravity = !OnSlope();

        float multiplier = _grounded ? 1 : _airMultiplier;
        _rigidbody.AddForce(direction * _moveSpeed * 10 * multiplier);

        SpeedControl();

        if (Input.GetKey(KeyCode.Space) && _canJump && _grounded)
        {
            Jump();
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        if (OnSlope()) flatVel = _rigidbody.velocity;

        if (flatVel.magnitude > _moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        _canJump = false;

        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

        Invoke(nameof(ResetJump), _jumpCooldown);
    }

    private void ResetJump()
    {
        _canJump = true;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down, out _slopeHit, _height * 0.5f + 0.4f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
    }
}
