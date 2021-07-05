using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Quaternion camRotation;
    [SerializeField] private float Sensitivity;
    private float clampangle = 80f;
    
    // Start is called before the first frame update
    void Start()
    {
        camRotation = transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camRotation.x += Input.GetAxis("Mouse Y") * Sensitivity;
        camRotation.y += Input.GetAxis("Mouse X") * Sensitivity;

        camRotation.x = Mathf.Clamp(camRotation.x,-clampangle ,clampangle );

        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
    }
}
