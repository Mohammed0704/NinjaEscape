﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    public float cameraSpeed = 20.0f; // Speed at which the camera moves

    private Vector3 startPosition; // Initial position of the camera

    private void Start()
    {
        // Store the initial position of the camera
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = new Vector3(transform.position.x + cameraSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        // Move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);

        // Check if the player is within the camera's view
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) // Only perform the check if the player game object exists
        {
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
            {
                // Destroy the player game object if it is not within the camera's view
                Destroy(player);
            }
        }
    }
}

/*
public class CamerController : MonoBehaviour {

    public float speed;

    public float clampLeft;

    public float clampRight;


    private float cameraX;


    // Use this for initialization

    void Start () {

        cameraX = transform.position.x;

		
	}

	
	// Update is called once per frame

	void Update () {

        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < clampRight)

        {

            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > clampLeft)

        {

            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));

        }

        if (Input.GetKey(KeyCode.Space))

        {

            Debug.Log(cameraX);

        }

    }

}
*/

