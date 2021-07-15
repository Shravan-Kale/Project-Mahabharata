using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using Random = UnityEngine.Random;

public class BowController : WeaponUtilities
{
    [Space] [SerializeField] private GameObject arrowPrefab; // TODO: load from resources
    [SerializeField] private float offset;

    [Space] [SerializeField] private float forceIncrease = 1f;
    [SerializeField] private float maxForce = 20f;
    [SerializeField] private float forceUpdateFrequency = 20;

    // line renderer
    [Space] [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int numPoints = 50;
    [SerializeField] private float timeBetweenPoints = 0.1f;
    [SerializeField] private LayerMask collidableLayers;
    [SerializeField] private float collideRadius = 1f; // works with collidableLayers

    [Space] [SerializeField] private string recoilState;
    [SerializeField] private GameObject _TEST;
    [SerializeField] private GameObject _TEST2;

    // local variables
    // Instantiate arrow
    private Vector3 _firePoint;
    private GameObject _arrow;
    private bool _drawTrajectory = false;

    // Line renderer
    private List<Vector3> _points;
    private Vector3 _startingVelocity;
    private Vector3 _newPoint;
    private float _currentForce = 0;

    private IEnumerator _shootCoroutine;
    private Transform _firePointTransform;

    private void Awake()
    {
        SetUpUtils();
        _shootCoroutine = IncreaseForce();
        _firePointTransform = transform.Find("Fire point");
    }

    private void Update()
    {
        _firePoint = CalculateFirePoint();
        DrawTrajectory();
        Debug.Log(_TEST2.transform.forward);
    }

#region DrawTrajectory

    private void DrawTrajectory()
    {
        if (_drawTrajectory == false)
        {
            if (_lineRenderer.enabled)
                _lineRenderer.enabled = false;

            return;
        }
        else
        {
            if (_lineRenderer.enabled == false)
                _lineRenderer.enabled = true;
        }

        _lineRenderer.positionCount = numPoints;
        _points = new List<Vector3>();
        _startingVelocity = _firePoint + _TEST2.transform.forward * _currentForce;

        for (float i = 0; i < numPoints; i += timeBetweenPoints)
        {
            _newPoint = _firePoint + i * _startingVelocity;
            _newPoint.y = _firePoint.y + _startingVelocity.y * i + Physics.gravity.y / 2f * i * i;
            _points.Add(_newPoint);

            if (Physics.OverlapSphere(_newPoint, collideRadius, collidableLayers).Length > 0)
            {
                _lineRenderer.positionCount = _points.Count;
                break;
            }
        }

        _lineRenderer.SetPositions(_points.ToArray());
    }

#endregion

#region Shoot

    public void StartShootCoroutine()
    {
        _drawTrajectory = true;
        StartCoroutine(_shootCoroutine);
    }

    private IEnumerator IncreaseForce()
    {
        for (;;)
        {
            _currentForce += Time.deltaTime * forceIncrease;
            _currentForce = _currentForce > maxForce ? maxForce : _currentForce;
            yield return new WaitForSeconds(1f / forceUpdateFrequency);
        }
    }

    public void Shoot() // Invoke on button up
    {
        StopCoroutine(_shootCoroutine);
        _arrow = Instantiate(arrowPrefab, _firePoint, transform.rotation);
        _arrow.GetComponent<Rigidbody>().velocity = _firePoint * _currentForce;
        _currentForce = 0;
        _drawTrajectory = false;
        PlayerAnimatorController.playerAnimator.Play(recoilState);
    }

#endregion

    private Vector3 CalculateFirePoint()
    {
        return _TEST.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_firePoint + _TEST.transform.forward * _currentForce, 0.1f);
    }
}