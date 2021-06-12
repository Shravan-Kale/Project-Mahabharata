using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CsGlobal : MonoBehaviour
{
    [SerializeField] private UnityEvent onLeftClick;

    private void Awake()
    {
        if (onLeftClick == null)
            onLeftClick = new UnityEvent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            onLeftClick.Invoke();
    }
}