using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enlarger : MonoBehaviour
{
	public float EnlargeSpeed;
	public float MaxSize;

	private void Start()
	{
		
	}

	private void Update()
	{
		/*
		  if grounded
			if val > MAX
			  enlarge
		*/
	    float currentSize = transform.localScale.x;
	    currentSize += EnlargeSpeed * Time.deltaTime;

	}

	private void Shrink(float amount)
	{
		//if > 0


	}
}
