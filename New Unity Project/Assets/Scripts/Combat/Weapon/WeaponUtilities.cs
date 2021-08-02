using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponUtilities : MonoBehaviour
{
    [SerializeField] public WeaponTypes weaponType;
    [Space] [SerializeField] public GameObject weaponContainer;
    [Space] [SerializeField] public WeaponComboStats[] weaponComboStatsArray;
    [SerializeField] public float comboResetTimer;

    // public variables
    [HideInInspector]public int currentComboIndex = 0;

    //  protected variables
    protected Collider _collider;
    protected Rigidbody _rb;
    protected float previousTime;
    protected int comboStates;

    public enum WeaponTypes
    {
        melee,
        bow
    }

    public void InvokeAttackAnimation()
    {
        if (Time.time - previousTime < comboResetTimer)
        {
            currentComboIndex++;
        }
        else
        {
            currentComboIndex = 0;
        }

        previousTime = Time.time;

        if (currentComboIndex == weaponComboStatsArray.Length)
            currentComboIndex = 0;

        PlayerAnimatorController.playerAnimator.Play(weaponComboStatsArray[currentComboIndex].animationsStateName);
    }

    protected void SetUpUtils()
    {
        previousTime = Time.time;
        _collider = transform.GetComponent<Collider>();
        _rb = transform.GetComponent<Rigidbody>();
    }

    public void PickUp(Transform weaponContainerTransform)
    {
        ChangeComponentActive(false);

        transform.position = weaponContainerTransform.position;
        transform.rotation = weaponContainerTransform.rotation;
        transform.SetParent(weaponContainerTransform);
    }

    public void Drop(Vector3 dropForce)
    {
        transform.parent = null;

        ChangeComponentActive(true);
        _rb.AddForce(dropForce);
    }

    private void ChangeComponentActive(bool isActive)
    {
        _rb.isKinematic = isActive == false;
        _rb.useGravity = isActive;
        _collider.enabled = isActive;
    }

    [Serializable]
    public class WeaponComboStats
    {
        public string animationsStateName;
        public float attackSpeed;
        public int damage;
    }
}