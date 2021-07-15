using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUtilities : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public WeaponTypes weaponType;
    [Space] [SerializeField] public GameObject weaponContainer;
    [Space][SerializeField] public string animationStateName;

    // internal variables
    internal Collider _collider;
    internal Rigidbody _rb;
    
    public enum WeaponTypes
    {
        melee,
        bow
    }

    public void InvokeAttackAnimation()
    {
        PlayerAnimatorController.playerAnimator.Play(animationStateName);
    }
    
    public void SetUpUtils()
    {
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
}
