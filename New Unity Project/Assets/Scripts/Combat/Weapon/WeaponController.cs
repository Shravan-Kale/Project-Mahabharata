using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
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


    private void Awake()
    {
        _camera = FindObjectOfType<Camera>().transform;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + textInstantiateOffset, gizmosSphereRadius);
    }
}
