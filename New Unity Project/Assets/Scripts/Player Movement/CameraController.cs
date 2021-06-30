using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Transform targetTransform;

    private Camera _Cam;
    public Camera cam;
    [SerializeField]private Vector3 CamOffset = Vector3.zero;
    [SerializeField]private Vector3 ZoomOffset = Vector3.zero;
    [SerializeField]private float senstivityX = 5;
    [SerializeField]private float senstivityY = 1;
    [SerializeField]private float minY = 30;
    [SerializeField]private float maxY = 50;
    [SerializeField]private bool isZooming;
    private float currentX = 0;
    private float currentY = 1;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        currentX += Input.GetAxisRaw("Mouse X");
        currentY -= Input.GetAxisRaw("Mouse Y");

        currentX = Mathf.Repeat(currentX, 360);
        currentY = Mathf.Clamp(currentY, minY, maxY);

        isZooming = Input.GetMouseButton(1);
    }
    void LateUpdate()
    {
        Vector3 dist = isZooming? ZoomOffset : CamOffset;
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = targetTransform.position + rotation * dist;
        transform.LookAt(targetTransform.position);
        CheckWall();
    }
    [SerializeField]private LayerMask wallLayer;
    void CheckWall()
    {
        RaycastHit hit;
        Vector3 start = targetTransform.position;
        Vector3 dir = transform.position - targetTransform.position;
        float dist = CamOffset.z * -1;
        Debug.DrawRay(targetTransform.position, dir, Color.green);
        if(Physics.Raycast(targetTransform.position, dir, out hit, dist, wallLayer))
        {
            float hitDist = hit.distance;
            Vector3 sphereCastCenter =  targetTransform.position + (dir.normalized * hitDist);
            transform.position = sphereCastCenter;
        }
    }

}
