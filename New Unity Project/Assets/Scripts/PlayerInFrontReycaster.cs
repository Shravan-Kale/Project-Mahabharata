using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInFrontReycaster : MonoBehaviour
{
    [SerializeField] private float pickUpDistance;
    [SerializeField] private Vector3 headOffset;

    // local variables
    private Camera _camera;
    private RaycastHit[] _raycastInfo;
    private Weapon _weapon;

    private void Awake()
    {
        _camera = Camera.main;
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
                    PlayerInventoryController.PickUp(_weapon);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position + headOffset,
                      // transform.position - _camera.transform.position);
    }
}