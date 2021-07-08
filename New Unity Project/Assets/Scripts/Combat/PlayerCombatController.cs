using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private float attackDistance;
    [Space] [SerializeField] private float handDamage = 10f;
    [SerializeField] private float attackSpeed = 0.5f;

    // local variables
    private Transform _playerTransform;
    private RaycastHit _raycastHit;
    private bool canAttack = true;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // invoke on left click
    public void Attack()
    {
        if (canAttack == false)
            return;
        
        PlayerAnimatorController.TriggerAttackAnimation();

        if (Physics.Raycast(_playerTransform.position + attackOffset,
                            _playerTransform.forward,
                            out _raycastHit,
                            attackDistance))
        {
            if (_raycastHit.collider.CompareTag("Enemy"))
            {
                if (PlayerInventoryController.isBareHanded)
                {
                    _raycastHit.transform.GetComponent<EnemyHealth>().GetDamage(handDamage);
                }
            }
        }
        
        StartCoroutine(AttackCD());


        IEnumerator AttackCD()
        {
            canAttack = false;
            yield return new WaitForSeconds(1f / attackSpeed);
            canAttack = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
        {
            Gizmos.DrawSphere(attackOffset, 0.2f);
        }

        if (Application.isPlaying)
        {
            Gizmos.DrawLine(_playerTransform.position + attackOffset,
                            _playerTransform.position +
                            attackOffset +
                            _playerTransform.forward * attackDistance);
        }
    }
}