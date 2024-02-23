using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    [field: SerializeField] public WeaponType WeaponType { get; private set; }
    [SerializeField] ParticleSystem _hitParticles;

    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<RecieveDmgTest>();
        enemy.TakeDamage();

        _hitParticles.transform.position = collision.contacts[0].point;
        _hitParticles.transform.rotation = Quaternion.LookRotation(transform.position - enemy.transform.position);
        _hitParticles.Play();
    }

    void Update()
    {
        
    }

    public void EnableCollider(bool enable)
    {
        _collider.enabled = enable;
    }

}
