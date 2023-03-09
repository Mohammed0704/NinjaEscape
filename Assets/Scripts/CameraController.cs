using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Old Script where it is camera move at constant speed, plus follow player y position

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
        // Find the player game object
        GameObject player = GameObject.FindWithTag("Player");

        // Only perform camera movement if the player game object exists
        if (player != null)
        {
            // Calculate the target position for the camera
            Vector3 targetPosition = new Vector3(transform.position.x + cameraSpeed * Time.deltaTime, player.transform.position.y, transform.position.z);

            // Move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);

            // Check if the player is within the camera's view
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
            {
                // Destroy the player game object if it is not within the camera's view
                Destroy(player);
            }
        }
    }
}
*/

public class CameraController : MonoBehaviour 
{
    private GameObject player; // Reference to the player game object
    public float damping = 0.2f; // Damping factor for camera movement

    private Vector3 velocity = Vector3.zero; // Reference velocity for SmoothDamp

    private void Start()
    {
        // Find the player game object
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        // Only perform camera movement if the player game object exists
        if (player != null)
        {
            // Calculate the target position for the camera
            Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

            // Move the camera towards the target position using SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);
        }
    }

}



/*
public class CameraController : MonoBehaviour 
{
public float cameraSpeed = 20.0f; // Speed at which the camera moves
private GameObject player; // Reference to the player game object

private void Start()
{
    // Find the player game object
    player = GameObject.FindWithTag("Player");
}

private void Update()
{
    // Only perform camera movement if the player game object exists
    if (player != null)
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        // Move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
}
*/