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
    [SerializeField] private GameObject swordHandContainerGOSerialization;
    [SerializeField] private float dropForce = 100f;

    // public static variables
    public static WeaponUtilities[] _itemsInHands = new WeaponUtilities[2]; // 0 - hand, 1 - back
    public static bool isBareHanded = true;

    // private static variables
    private static GameObject _swordHandContainerGO;
    
    private void Awake()
    {
        _swordHandContainerGO = swordHandContainerGOSerialization;
    }

    private void Update()
    {
        if (CsGlobal.isPressingQ)
            Drop();
    }

    public static void PickUp(WeaponUtilities weaponUtils)
    {
        if (_itemsInHands[0] == null)
        {
            // TODO: invoke pickup animation
            weaponUtils.PickUp(_swordHandContainerGO.transform);
            _itemsInHands[0] = weaponUtils;
            isBareHanded = false;
        }
    }

    public void Drop()
    {
        // TODO: drops in wrong direction
        if (_itemsInHands[0] != null)
        {
            _itemsInHands[0].Drop((transform.position + transform.forward) * dropForce);
            _itemsInHands[0] = null;
            isBareHanded = true;
            // TODO: invoke drop animation
        }
    }
}