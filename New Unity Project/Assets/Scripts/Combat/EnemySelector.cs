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
    private Renderer _renderer;
    private bool _anyEnemyInView;
    private Camera _camera;
    private Plane[] _planes;

    // public static variables
    public static bool isTargetSelected = false;
    public static GameObject _closestEnemy;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        _camera = FindObjectOfType<Camera>();
    }

    public void SelectEnemy()
    {
        isTargetSelected = isTargetSelected == false;

        if (isTargetSelected == false)
            return;

        if (CalculateClosestEnemyInView() == false)
            CalculateClosestEnemy();
    }

    private bool CalculateClosestEnemyInView()
    {
        _anyEnemyInView = false;
        _distance = 0;

        _planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        
        foreach (var enemy in _enemies)
        {
            _renderer = enemy.GetComponent<Renderer>();

            if (GeometryUtility.TestPlanesAABB(_planes,_renderer.bounds))
            {
                GetDistance(enemy);
                _anyEnemyInView = true;
            }
        }

        return _anyEnemyInView;
    }

    private void CalculateClosestEnemy()
    {
        foreach (var enemy in _enemies)
        {
            GetDistance(enemy);
        }
    }

    private void GetDistance(GameObject enemy)
    {
        _currentDistance =
            Vector3.Distance(_playerTransform.position, enemy.transform.position);
        
        if (_currentDistance < _distance ||
            _distance == 0)
        {
            _distance = _currentDistance;
            _closestEnemy = enemy;
            Debug.Log(enemy.name + " " + _distance);
        }
    }
}