using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float Smoothing = 0.5f;
    public Vector3 Offset;

    private void FixedUpdate()
    {
        Vector3 desiredPos = Target.position + Offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, Smoothing);
        transform.position = smoothedPos;

        transform.LookAt(Target);
    }

}
