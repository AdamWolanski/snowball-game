using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public Transform[] Backgrounds;
    public float Smoothing = 1f;

    private float[] _parallaxAmount;
    private Transform _cameraTransform;
    private Vector3 _previousCamPosition;

    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

	private void Start()
	{
	    _previousCamPosition = _cameraTransform.position;
	    _parallaxAmount = new float[Backgrounds.Length];

	    for (int i = 0; i < Backgrounds.Length; ++i)
	    {
	        _parallaxAmount[i] = Backgrounds[i].position.z * -1;
	    }
	}
	
	private void Update()
    {
        for (int i = 0; i < Backgrounds.Length; ++i)
        {
            float parallax = (_previousCamPosition.x - _cameraTransform.position.x) * _parallaxAmount[i];
            float backgroundTargetPosX = Backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, Backgrounds[i].position.y, Backgrounds[i].position.z);

            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos,
                Smoothing * Time.deltaTime);
        }
        _previousCamPosition = _cameraTransform.position;
    }
}
