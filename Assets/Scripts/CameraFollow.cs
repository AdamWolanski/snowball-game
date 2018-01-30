using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float Smoothing = 0.5f;
    public Vector3 Offset;
    public float Shake = 0.0f;
    public float ShakeAmount = 0.7f;
    public float DecreaseFactor = 1.0f;

    private void Start()
    {
        Offset.z *= -1;
    }

    private void FixedUpdate()
    {
        if (!Target) return;
        Vector3 desiredPos = Target.position + Offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, Smoothing);
        smoothedPos.z = -10;
        transform.position = smoothedPos;

        if (Shake > 0)
        {
            var x = Camera.main.transform;
            Camera.main.transform.localPosition += Random.insideUnitSphere * ShakeAmount;
            x.position = new Vector3(x.position.x, x.position.y, -10);
            Shake -= Time.deltaTime * DecreaseFactor;
        }
        else
        {
            Shake = 0.0f;
        }
    }

}
