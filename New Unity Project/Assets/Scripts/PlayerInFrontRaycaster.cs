using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInFrontRaycaster : MonoBehaviour
{
    [SerializeField] private float pickUpDistance;
    [SerializeField] private Vector3 headOffset;

    // local variables
    private Camera _camera;
    private RaycastHit[] _raycastInfo;
    private Weapon _weapon;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        _raycastInfo = Physics.RaycastAll(transform.position + headOffset,
                                          transform.position - _camera.transform.position,
                                          pickUpDistance);

        foreach (var raycastHit in _raycastInfo)
        {
            _weapon = raycastHit.transform.GetComponent<Weapon>();
            if (_weapon)
            {
                _weapon.ShowText();

                if (CsGlobal.isPressingE)
                {
                    Debug.Log("Pick up");
                    PlayerInventoryController.PickUp(_weapon);
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            Gizmos.DrawRay(transform.position + headOffset,
                           transform.position - _camera.transform.position);
    }
}