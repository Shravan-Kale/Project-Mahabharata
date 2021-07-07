using System;
using System.Collections;
using System.Collections.Generic;
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
    private Vector3 rotation;

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
        ChangeRotation();
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
            _cameraController.transform.rotation =
                Quaternion.Euler(_cameraController.transform.rotation.eulerAngles.x -
                                 CsGlobal.mouseYAxis * sensitivity,
                                 _cameraController.transform.rotation.eulerAngles.y +
                                 CsGlobal.mouseXAxis * sensitivity,
                                 _cameraController.transform.rotation.eulerAngles.z);
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
                _camera.transform.position = _hitInfo.point;
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
            rotation = UnityEditor.TransformUtils.GetInspectorRotation(_cameraController.transform);

            if (rotation.x > maxXAngle ||
                rotation.x < minXAngle)
            {
                UnityEditor.TransformUtils.SetInspectorRotation(
                    _cameraController.transform,
                    new Vector3(Mathf.Clamp(rotation.x, minXAngle, maxXAngle),
                                rotation.y,
                                rotation.z));
            }
        }
    }
}