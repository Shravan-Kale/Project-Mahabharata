using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInFrontRaycaster : MonoBehaviour
{
    [SerializeField] private float pickUpDistance;
    [SerializeField] private Vector3 headOffset;

    // local variables
    private Camera _camera;
    private RaycastHit[] _raycastInfo;
    private WeaponController _weaponController;

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
            if (raycastHit.transform.CompareTag($"Weapon"))
            {
                _weaponController = raycastHit.transform.GetComponent<WeaponController>();
                _weaponController.ShowText();

                if (CsGlobal.isPressingE)
                {
                    PlayerInventoryController.PickUp(raycastHit.transform.GetComponent<WeaponUtilities>());
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