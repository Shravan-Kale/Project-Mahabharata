using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
    }
}
