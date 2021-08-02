using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private float scrollSensitivity = 1f;
    [Space] [SerializeField] private float yOffset = 3f;
    [Space] [SerializeField] private float zoomDefault = 3f;
    [SerializeField] private float zoomMax = 7f;
    [SerializeField] private float zoomMin = 1f;
    [Space] [SerializeField] private float collisionSensitivity = 1f;
    [SerializeField] private float characterCollisionSensitivity = 1f;
    [Space] [SerializeField] private float maxXAngle = 80f;
    [SerializeField] private float minXAngle = 50f;
    [Space] [SerializeField] private float lookAtTargetSpeed = 1;

    // local variables
    private GameObject _character;
    private GameObject _cameraController;
    private GameObject _cameraGO;
    private Camera _camera;
    private float _zoomDistance;
    private Vector3 _cameraDistance;
    private float _scrollAmount;
    private Vector3 _CollisionPoint;
    private RaycastHit _hitInfo;

    private Vector3 _rotation;

    // for target focusing
    private Vector3 _direction;
    private Quaternion _2Rotation;

    private void Awake()
    {
        _character = GameObject.FindGameObjectWithTag("Player");
        _camera = FindObjectOfType<Camera>();
        _cameraGO = _camera.gameObject;
        _cameraController = _cameraGO.transform.parent.gameObject;

        _zoomDistance = zoomDefault;
        _cameraDistance = _camera.transform.localPosition;
        _cameraDistance.z -= _zoomDistance;

        Cursor.visible = false;
    }

    void Update()
    {
        ChangePosition();
        if (EnemySelector.isTargetSelected == false)
            ChangeRotation();
        else
            LookAtTarget();
        ChangeZoom();
        CheckCollision();
        RestrictRotation();

        void ChangePosition()
        {
            _cameraController.transform.position = _character.transform.position +
                                                   new Vector3(0,
                                                               yOffset,
                                                               0);
            _camera.transform.localPosition = _cameraDistance;
        }

        void ChangeRotation()
        {
            var rotation = _cameraController.transform.rotation;
            rotation = Quaternion.Euler(rotation.eulerAngles.x -
                                        CsGlobal.mouseYAxis * sensitivity,
                                 rotation.eulerAngles.y +
                                 CsGlobal.mouseXAxis * sensitivity,
                                 rotation.eulerAngles.z);
            _cameraController.transform.rotation = rotation;
        }

        void LookAtTarget()
        {
//            _cameraController.transform.LookAt(EnemySelector._closestEnemy.transform);
            _direction = EnemySelector._closestEnemy.transform.position - transform.position;
            _2Rotation = Quaternion.LookRotation(_direction);
            transform.rotation =
                Quaternion.Lerp(transform.rotation, _2Rotation, lookAtTargetSpeed * Time.deltaTime);
        }

        void ChangeZoom()
        {
            if (CsGlobal.mouseWheelAxis != 0)
            {
                _scrollAmount = CsGlobal.mouseWheelAxis * scrollSensitivity;
                _zoomDistance += -_scrollAmount;
                _zoomDistance = Mathf.Clamp(_zoomDistance, zoomMin, zoomMax);
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (_cameraDistance.z != -_zoomDistance)
            {
                _cameraDistance.z = Mathf.Lerp(_cameraDistance.z, -_zoomDistance, Time.deltaTime);
            }
        }

        void CheckCollision()
        {
            if (Physics.Linecast(_cameraController.transform.position,
                                 _camera.transform.position -
                                 new Vector3(0, 0, collisionSensitivity),
                                 out _hitInfo))
            {
                _camera.transform.position =
                    _hitInfo.point + new Vector3(0, 0, collisionSensitivity);
            }

            if (_camera.transform.localPosition.z > -characterCollisionSensitivity)
            {
                _camera.transform.localPosition = new Vector3(0, 0, -characterCollisionSensitivity);
            }
        }

        void RestrictRotation()
        {
            _rotation =
                UnityEditor.TransformUtils.GetInspectorRotation(_cameraController.transform);

            if (_rotation.x > maxXAngle ||
                _rotation.x < minXAngle)
            {
                UnityEditor.TransformUtils.SetInspectorRotation(
                    _cameraController.transform,
                    new Vector3(Mathf.Clamp(_rotation.x, minXAngle, maxXAngle),
                                _rotation.y,
                                _rotation.z));
            }
        }
    }
}