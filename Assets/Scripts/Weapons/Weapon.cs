using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum WeaponType { Light, OneHanded, TwoHanded, OffHand }

public class Weapon : MonoBehaviour
{
    [field: SerializeField] public WeaponType Type { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float StartCharge { get; private set; }
    [field: SerializeField] public float BlockRate { get; private set; }

    [Space(7)]public RuntimeAnimatorController Animator;

    private Collider _collider;


    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<RecieveDmgTest>();
        enemy.TakeDamage();

        //_hitParticles.transform.position = collision.contacts[0].point;
        //_hitParticles.transform.rotation = Quaternion.LookRotation(transform.position - collision.transform.position);
        //_hitParticles.Play();
    }

    public void EnableCollider(bool enable)
    {
        if(_collider==null) _collider = GetComponent<Collider>();
        _collider.enabled = enable;
    }
}
