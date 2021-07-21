using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    // local variables
    private Transform _playerTransform;
    private List<GameObject> _enemies;
    private float _distance = 0;
    private float _currentDistance;
    
    // public static variables
    public static bool isTargetSelected = false;
    public static GameObject _closestEnemy;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    }

    private void Update()
    {
        Debug.Log(_closestEnemy);
    }

    public void SelectEnemy()
    {
        isTargetSelected = isTargetSelected == false;
        
        if (isTargetSelected == false)
            return;
        
        CalculateClosestEnemy();
    }
     
    private void CalculateClosestEnemy()
    {
        foreach (var enemy in _enemies)
        {
            _currentDistance = Vector3.Distance(_playerTransform.position, enemy.transform.position);
            if (_currentDistance < _distance || _distance == 0)
            {
                _distance = _currentDistance;
                _closestEnemy = enemy;
            }
        }
    }
}
