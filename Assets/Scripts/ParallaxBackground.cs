using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform[] Backgrounds;
    public float Smoothing;

    private Vector3 _previousCameraPosition;
    private float[] _parallaxScales;

    private void Start()
	{
	    _previousCameraPosition = transform.position;
        _parallaxScales = new float[Backgrounds.Length];
	    for (var i = 0; i < _parallaxScales.Length; ++i)
	    {
            _parallaxScales[i] = Backgrounds[i].position.z * -1;
	    }
	}
	
	private void LateUpdate()
    {
        for (var i = 0; i < Backgrounds.Length; ++i)
        {
            var parallax = (_previousCameraPosition - transform.position) * (_parallaxScales[i] / Smoothing);
            Backgrounds[i].position = new Vector3(Backgrounds[i].position.x + parallax.x, Backgrounds[i].position.y + parallax.y, Backgrounds[i].position.z);
        }

        _previousCameraPosition = transform.position;
    }
}
