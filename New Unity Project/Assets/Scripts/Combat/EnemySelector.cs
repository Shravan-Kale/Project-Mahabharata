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
    private List<GameObject> _selectedEnemies = new List<GameObject>();

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

    public void SelectAnotherEnemy()
    {
        if (isTargetSelected == false)
            return;

        _selectedEnemies.Add(_closestEnemy);
        if (_selectedEnemies.Count == _enemies.Count)
            _selectedEnemies.RemoveAt(0);

        if (CalculateClosestEnemyInView(true) == false)
            CalculateClosestEnemy(true);
    }

    private bool CalculateClosestEnemyInView(bool nextEnemy = false)
    {
        _anyEnemyInView = false;
        _distance = 0;

        _planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        foreach (var enemy in _enemies)
        {
            if (nextEnemy && CheckNextEnemy(enemy))
                continue;

            _renderer = enemy.GetComponent<Renderer>();

            if (GeometryUtility.TestPlanesAABB(_planes, _renderer.bounds))
            {
                GetDistance(enemy);
                _anyEnemyInView = true;
            }
        }

        return _anyEnemyInView;
    }

    private void CalculateClosestEnemy(bool nextEnemy = false)
    {
        foreach (var enemy in _enemies)
        {
            if (nextEnemy && CheckNextEnemy(enemy))
                continue;
            
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
        }
    }

    private bool CheckNextEnemy(GameObject enemy)
    {
        foreach (var selectedEnemy in _selectedEnemies)
        {
            if (enemy == selectedEnemy)
            {
                return true;
            }
        }

        return false;
    }
}