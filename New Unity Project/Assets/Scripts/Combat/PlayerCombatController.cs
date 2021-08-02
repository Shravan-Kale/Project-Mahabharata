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
    private bool _canAttack = true;
    private WeaponUtilities _weapon;
    private float _damage2Deal;
    private float _currenAttackCd;
    private BowController _bowController;
    private bool _isShootingCoroutineStarted = false;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // invoke on left click
    public void Attack()
    {
        if (_canAttack == false)
            return;

        if (PlayerInventoryController.isBareHanded)
        {
            PlayerAnimatorController.playerAnimator.Play("Punching");
            _damage2Deal = handDamage;
            _currenAttackCd = attackSpeed;
        }
        else
        {
            _weapon = PlayerInventoryController._itemsInHands[0]
                .GetComponent<WeaponUtilities>();
            
            if (_weapon.weaponType == WeaponUtilities.WeaponTypes.melee)
            {
                PlayerInventoryController._itemsInHands[0].InvokeAttackAnimation();

                SetUpWeapon();
                DoMeleeDamage();

                StartCoroutine(AttackCD(_currenAttackCd));
            }
            else if (_weapon.weaponType == WeaponUtilities.WeaponTypes.bow)
            {
                SetUpWeapon();
                
                DoBowDamage();
            }
        }

        void SetUpWeapon()
        {
            _damage2Deal = _weapon.weaponComboStatsArray[_weapon.currentComboIndex].damage;
            _currenAttackCd = _weapon.weaponComboStatsArray[_weapon.currentComboIndex].attackSpeed;
        }
        
        void DoMeleeDamage()
        {
            if (Physics.Raycast(_playerTransform.position + attackOffset,
                _playerTransform.forward,
                out _raycastHit,
                attackDistance))
            {
                if (_raycastHit.collider.CompareTag("Enemy"))
                {
                    _raycastHit.transform.GetComponent<EnemyHealth>().GetDamage(_damage2Deal);
                }
            }
        }

        void DoBowDamage()
        {
            _bowController = _weapon.GetComponent<BowController>();
            _bowController.StartShootCoroutine();
            _isShootingCoroutineStarted = true;
            PlayerAnimatorController.playerAnimator.Play(
                _bowController.weaponComboStatsArray[0].animationsStateName); // TODO: mb make bow combo
        }
    }

    // invoke on button up
    public void BowShot()
    {
        if (_canAttack == false || _isShootingCoroutineStarted == false)
            return;

        if (_weapon.weaponType == WeaponUtilities.WeaponTypes.bow)
        {
            _bowController.Shoot();
            _isShootingCoroutineStarted = false;
            StartCoroutine(AttackCD(_currenAttackCd));
        }
    }

    IEnumerator AttackCD(float time2Wait)
    {
        if (time2Wait == 0)
            Debug.LogError("ERROR");

        _canAttack = false;
        yield return new WaitForSeconds(1f / time2Wait);
        _canAttack = true;
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