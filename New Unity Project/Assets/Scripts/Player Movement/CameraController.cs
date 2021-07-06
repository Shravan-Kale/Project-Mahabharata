using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float distanceBehind;
    [SerializeField] private float distanceAbove;
    [SerializeField] private Vector3 lookOffset;

    // local variables
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = _playerTransform.position -
                             (_playerTransform.forward * distanceBehind) +
                             (_playerTransform.up * distanceAbove);
        transform.LookAt(_playerTransform.position + lookOffset);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            Gizmos.DrawSphere(_playerTransform.position -
                              (_playerTransform.forward * distanceBehind) +
                              (_playerTransform.up * distanceAbove),
                              1f);
    }
}