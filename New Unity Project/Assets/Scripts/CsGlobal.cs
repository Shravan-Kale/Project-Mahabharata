using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CsGlobal : MonoBehaviour
{
    [SerializeField] private UnityEvent onLeftClick;

    public static float horizontalRawAxis;
    public static float verticalRawAxis;
    public static bool isPlayerMoving;
    public static bool isPressingE;
    public static bool isPressingQ;
    public static float mouseYAxis;
    public static float mouseXAxis;
    public static float mouseWheelAxis;

    private void Awake()
    {
        onLeftClick ??= new UnityEvent();
        if (onLeftClick == null)
            onLeftClick = new UnityEvent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            onLeftClick.Invoke();

        isPressingE = Input.GetKeyDown(KeyCode.E);
        isPressingQ = Input.GetKeyDown(KeyCode.Q);

        horizontalRawAxis = Input.GetAxisRaw("Horizontal");
        verticalRawAxis = Input.GetAxisRaw("Vertical");

        mouseYAxis = Input.GetAxis("Mouse Y");
        mouseXAxis = Input.GetAxis("Mouse X");
        mouseWheelAxis = Input.GetAxis("Mouse ScrollWheel");

        isPlayerMoving = horizontalRawAxis != 0 || verticalRawAxis != 0;
    }
}