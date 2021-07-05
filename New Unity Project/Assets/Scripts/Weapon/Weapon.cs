using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject textPrefabGO;
    [SerializeField] private float time2DeactivateText = 0.1f;
    [SerializeField] private Vector3 textInstantiateOffset;
    [Space] [SerializeField] private float gizmosSphereRadius  = 0.1f;

    // local variables
    private bool _textWasInstantiated = false;
    private Transform _camera;
    private float _currentTime = 0;
    private GameObject _currentTextGO;
    private Collider _collider;
    private Rigidbody _rb;

    private void Awake()
    {
        _camera = Camera.main.transform;
        _collider = transform.GetComponent<Collider>();
        _rb = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _currentTime += Time.fixedDeltaTime;

        if (_currentTime > time2DeactivateText)
        {
            Destroy(_currentTextGO);
            _textWasInstantiated = false;
        }
    }

    public void ShowText()
    {
        Debug.Log("Show text");

        _currentTime = 0;

        if (_textWasInstantiated == false)
        {
            _currentTextGO = Instantiate(textPrefabGO,
                                         transform.position + textInstantiateOffset,
                                         Quaternion.identity);
            _textWasInstantiated = true;
        }
        
        _currentTextGO.transform.LookAt(_camera);
    }
    
    public void PickUp(Transform weaponContainerTransform)
    {
        transform.position = weaponContainerTransform.position;
        transform.SetParent(weaponContainerTransform);
        _rb.useGravity = false;
        _collider.enabled = false;
    }

    public void Drop(Vector3 dropForce)
    {
        transform.parent = null;
        _rb.useGravity = true;
        _collider.enabled = true;
        _rb.AddForce(dropForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + textInstantiateOffset, gizmosSphereRadius);
    }
}