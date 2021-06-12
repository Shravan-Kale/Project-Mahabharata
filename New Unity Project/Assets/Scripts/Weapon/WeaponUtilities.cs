using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUtilities : MonoBehaviour
{
    [SerializeField] private float offset;

    protected Vector3 CalculateFirePoint()
    {
        return transform.position + transform.forward * offset;
    }

#region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + transform.forward * offset, 0.1f);
    }

#endregion
}
