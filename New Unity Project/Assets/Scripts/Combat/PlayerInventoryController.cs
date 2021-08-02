using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] private float dropForceSerializable = 100f;

    // public static variables
    public static WeaponUtilities[] _itemsInHands = new WeaponUtilities[2]; // 0 - hand, 1 - back
    public static bool isBareHanded = true;

    // private static variables
    private static GameObject _weaponContainerGO;
    private static float dropForce;
    private static Transform playerTransform;

    private void Awake()
    {
        dropForce = dropForceSerializable;
        playerTransform = transform;
    }
    
    private void Update()
    {
        if (CsGlobal.isPressingQ)
            Drop();
    }

    public static void PickUp(WeaponUtilities weaponUtils)
    {
        if ((_itemsInHands[0] == null) == false)
        {
            Drop();
        }
        
        // TODO: invoke pickup animation
        _weaponContainerGO = weaponUtils.weaponContainer;
        weaponUtils.PickUp(_weaponContainerGO.transform);
        _itemsInHands[0] = weaponUtils;
        isBareHanded = false;
    }

    public static void Drop()
    {
        // TODO: drops in wrong direction
        if (_itemsInHands[0] != null)
        {
            _itemsInHands[0].Drop((playerTransform.position + playerTransform.forward) * dropForce);
            _itemsInHands[0] = null;
            isBareHanded = true;
            // TODO: invoke drop animation
        }
    }
}