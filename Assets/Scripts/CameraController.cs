using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    public float cameraSpeed = 20.0f; // Speed at which the camera moves
    public float cameraOffset = 2.0f; // Offset from the player's position

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


