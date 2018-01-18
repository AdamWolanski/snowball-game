using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float Smoothing = 0.5f;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - Target.position;
        _offset.z *= -1;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPos = Target.position + _offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, Smoothing);
        smoothedPos.z = -10;
        transform.position = smoothedPos;

        //transform.LookAt(Target);
    }

}
