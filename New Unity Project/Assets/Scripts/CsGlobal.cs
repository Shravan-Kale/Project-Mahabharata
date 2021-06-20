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

    private void Awake()
    {
        onLeftClick ??= new UnityEvent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            onLeftClick.Invoke();

        horizontalRawAxis = Input.GetAxisRaw("Horizontal");
        verticalRawAxis = Input.GetAxisRaw("Vertical");

        isPlayerMoving = horizontalRawAxis != 0 || verticalRawAxis != 0;
    }
}