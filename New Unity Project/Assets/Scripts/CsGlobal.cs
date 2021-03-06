using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class CsGlobal : MonoBehaviour
{
    [SerializeField] private UnityEvent onLeftClick;
    [SerializeField] private UnityEvent leftMouseUp;
    [SerializeField] private UnityEvent XButtonClick;
    [SerializeField] private UnityEvent ZButtonClick;

    public static float horizontalRawAxis;
    public static float verticalRawAxis;
    public static float mouseYAxis;
    public static float mouseXAxis;
    public static float mouseWheelAxis;
    public static bool isPlayerMoving;
    public static bool isPressingE;
    public static bool isPressingQ;
    public static bool isPressingShift;
    public static bool isPressingControl;

    private void Awake()
    {
        onLeftClick ??= new UnityEvent();
        leftMouseUp ??= new UnityEvent();
        XButtonClick ??= new UnityEvent();
        ZButtonClick ??= new UnityEvent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            onLeftClick.Invoke();
        
        if (Input.GetButtonUp("Fire1"))
            leftMouseUp.Invoke();

        if (Input.GetKeyDown(KeyCode.X))
            XButtonClick.Invoke();
        
        if (Input.GetKeyDown(KeyCode.Z))
            ZButtonClick.Invoke();

        isPressingE = Input.GetKeyDown(KeyCode.E);
        isPressingQ = Input.GetKeyDown(KeyCode.Q);
        isPressingShift = Input.GetKey(KeyCode.LeftShift);
        isPressingControl = Input.GetKey(KeyCode.LeftControl);

        horizontalRawAxis = Input.GetAxisRaw("Horizontal");
        verticalRawAxis = Input.GetAxisRaw("Vertical");

        mouseYAxis = Input.GetAxis("Mouse Y");
        mouseXAxis = Input.GetAxis("Mouse X");
        mouseWheelAxis = Input.GetAxis("Mouse ScrollWheel");

        isPlayerMoving = horizontalRawAxis != 0 || verticalRawAxis != 0;
    }
}