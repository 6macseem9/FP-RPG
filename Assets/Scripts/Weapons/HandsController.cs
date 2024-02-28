using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    [SerializeField] private Weapon _rightWeapon;
    [SerializeField] private Weapon _leftWeapon;

    private WeaponController _rHand;
    private WeaponController _lHand;

    private void Start()
    {
        var hands = GetComponentsInChildren<WeaponController>();
        _rHand = hands[0];
        _lHand = hands[1];

        _rHand.SetWeapon(_rightWeapon);
        _lHand.SetWeapon(_leftWeapon);

        SetControlls();
    }

    private void SetControlls()
    {
        _rHand.AttackButton = -1;
        _rHand.BlockButton = -1;
        _lHand.AttackButton = -1;
        _lHand.BlockButton = -1;

        if (_rHand.HasWeapon && _lHand.HasWeapon)
        {
            if (_rHand.Weapon.Type == WeaponType.OffHand) _rHand.BlockButton = 0;
            else _rHand.AttackButton = 0;

            if (_lHand.Weapon.Type==WeaponType.OffHand) _lHand.BlockButton = 1;
            else _lHand.AttackButton = 1;
        }
        if (_rHand.HasWeapon && !_lHand.HasWeapon)
        {
            _rHand.AttackButton = 0;
            _rHand.BlockButton = 1;
        }
        if (!_rHand.HasWeapon && _lHand.HasWeapon)
        {
            _lHand.AttackButton = 0;
            _lHand.BlockButton = 1;
        }
    }

    private void Update()
    {
        if(_rHand.HasWeapon) _rHand.AllowUpdate = _lHand.InIdleState;
        if(_lHand.HasWeapon) _lHand.AllowUpdate = _rHand.InIdleState;
    }
}
