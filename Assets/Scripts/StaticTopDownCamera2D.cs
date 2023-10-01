using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTopDownCamera2D : MonoBehaviour
{
    public float offsetX = 1.0f; // Offset from the right edge of the screen
    public float offsetY = 1.0f; // Offset from the top edge of the screen

    private void Start()
    {
        // Calculate the desired camera position based on screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        Vector3 cameraPosition = new Vector3(screenWidth - offsetX, screenHeight - offsetY, -10);

        // Set the camera's position to the calculated position
        transform.position = cameraPosition;
    }
}
