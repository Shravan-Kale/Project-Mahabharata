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
    private WeaponUtilities _weapon;
    private float damage2Deal;
    private float currenAttackCD;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // invoke on left click
    public void Attack()
    {
        if (canAttack == false)
            return;

        if (PlayerInventoryController.isBareHanded)
        {
            PlayerAnimatorController.playerAnimator.Play("Punching");
            damage2Deal = handDamage;
            currenAttackCD = attackSpeed;
        }
        else
        {
            PlayerInventoryController._itemsInHands[0].InvokeAttackAnimation();
            _weapon = PlayerInventoryController._itemsInHands[0]
                                               .GetComponent<WeaponUtilities>();
            damage2Deal = _weapon.damage;
            damage2Deal = _weapon.attackSpeed;
        }

        if (Physics.Raycast(_playerTransform.position + attackOffset,
                            _playerTransform.forward,
                            out _raycastHit,
                            attackDistance))
        {
            if (_raycastHit.collider.CompareTag("Enemy"))
            {
                _raycastHit.transform.GetComponent<EnemyHealth>().GetDamage(damage2Deal);
            }
        }

        StartCoroutine(AttackCD(currenAttackCD));

        IEnumerator AttackCD(float time2Wait)
        {
            if (time2Wait == 0)
                Debug.LogError("ERROR");
            
            canAttack = false;
            yield return new WaitForSeconds(1f / time2Wait);
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