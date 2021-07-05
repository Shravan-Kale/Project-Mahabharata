using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float mindistance = 1.0f;
    public float maxdistance = 4.0f;
    public float smooth = 10.0f;
    Vector3 dollydir;
    public Vector3 dollydiradjusted9;
    public float distance;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        dollydir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredcamerapos = transform.parent.TransformPoint(dollydir * maxdistance);
        RaycastHit hit;
        
        if (Physics.Linecast(transform.parent.position,desiredcamerapos,out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.9f), mindistance, maxdistance);
        }
        else
        {
            distance = maxdistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollydir * distance, Time.deltaTime * smooth);
    }
}
