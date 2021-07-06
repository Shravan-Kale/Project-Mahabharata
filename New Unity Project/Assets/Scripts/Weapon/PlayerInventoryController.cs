using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private GameObject weaponHandContainerGOSerialization;
    [SerializeField] private float dropForce = 100f;

    // public static variables
    private static Weapon[] _itemsInHands = new Weapon[2]; // 0 - hand, 1 - back

    // private static variables
    private static GameObject _weaponHandContainerGO;
    
    private void Awake()
    {
        _weaponHandContainerGO = weaponHandContainerGOSerialization;
    }

    private void Update()
    {
        if (CsGlobal.isPressingQ)
            Drop();
    }

    public static void PickUp(Weapon _weapon)
    {
        if (_itemsInHands[0] == null)
        {
            // TODO: invoke pickup animation
            _weapon.PickUp(_weaponHandContainerGO.transform);
            _itemsInHands[0] = _weapon;
        }
    }

    public void Drop()
    {
        if (_itemsInHands[0] != null)
        {
            _itemsInHands[0].Drop((transform.position + transform.forward) * dropForce);
            _itemsInHands[0] = null;
            // TODO: invoke drop animation
        }
    }
}