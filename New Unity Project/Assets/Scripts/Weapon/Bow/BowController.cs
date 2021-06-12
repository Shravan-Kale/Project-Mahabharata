using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class BowController : WeaponUtilities
{
    [SerializeField] private GameObject arrowPrefab; // TODO: load from resources
    [SerializeField] private float arrowForce = 10f;

    // line renderer
    [Space] [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int numPoints = 50;
    [SerializeField] private float timeBetweenPoints = 0.1f;
    [SerializeField] private LayerMask collidableLayers;
    [SerializeField] private float collideRadius = 1f; // works with collidableLayers

    // local variables
    // Instantiate arrow
    private Vector3 _firePoint;
    private GameObject _arrow;

    // Line renderer
    private List<Vector3> points;
    private Vector3 startingVelocity;
    private Vector3 newPoint;

    private void Awake()
    {
        _lineRenderer.positionCount = numPoints;
    }

    private void Update()
    {
        _firePoint = CalculateFirePoint();
        DrawTrajectory();
    }

#region DrawTrajectory

    private void DrawTrajectory()
    {
        points = new List<Vector3>();
        startingVelocity = transform.forward * arrowForce;

        for (float i = 0; i < numPoints; i += timeBetweenPoints)
        {
            newPoint = _firePoint + i * startingVelocity;
            newPoint.y = _firePoint.y + startingVelocity.y * i + Physics.gravity.y / 2f * i * i;
            points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 2, collidableLayers).Length > 0)
            {
                Debug.Log("col");
                _lineRenderer.positionCount = points.Count;
                break;
            }
        }
        
        _lineRenderer.SetPositions(points.ToArray());
    }

#endregion

#region InstantiateArrow

    // Invoke on left click
    public void OnClick() // Instantiate arrow with set velocity
    {
        _arrow = Instantiate(arrowPrefab, _firePoint, transform.rotation);
        _arrow.GetComponent<Rigidbody>().velocity = transform.forward * arrowForce;
    }

#endregion
}