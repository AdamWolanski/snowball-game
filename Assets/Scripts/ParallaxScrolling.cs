﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {

    public float BackgroundSize;
    public float ParallaxSpeed;
    public bool LockY = false;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;

	private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;
	}

	private void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        //transform.position += Vector3.right * (deltaX * ParallaxSpeed);
        transform.position += Vector3.right * (deltaX * ParallaxSpeed);
        if (LockY)
            transform.position = new Vector2(transform.position.x, cameraTransform.position.y);
        lastCameraX = cameraTransform.position.x;

        //if (Input.GetKeyDown(KeyCode.A))
        //    ScrollLeft();
        //if (Input.GetKeyDown(KeyCode.D))
        //    ScrollRight();

        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
            ScrollLeft();
        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
            ScrollRight();
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - BackgroundSize);
        layers[rightIndex].position = new Vector2(layers[rightIndex].position.x, cameraTransform.position.y);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + BackgroundSize);
        layers[leftIndex].position = new Vector2(layers[leftIndex].position.x, cameraTransform.position.y);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;

    }
}
