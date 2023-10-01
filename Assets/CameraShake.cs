using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform; // The camera's Transform component
    public float shakeDuration = 0.5f; // The duration of the camera shake
    public float maxRotationAmount = 10f; // The maximum rotation amount during the shake
    public float eventTime;

    private Quaternion originalRotation; // The original rotation of the camera
    private float shakeTimer = 0f; // Timer for the shake

    public static CameraShake instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Ensure that the cameraTransform is set
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }

        // Save the original rotation of the camera
        originalRotation = cameraTransform.rotation;
    }

    private void Update()
    {
        // If the timer is greater than 0, shake the camera's rotation
        if (shakeTimer > 0)
        {
            // Create a random rotation within the specified range
            float randomRotation = Random.Range(-maxRotationAmount, maxRotationAmount);

            // Apply the random rotation to the camera's rotation
            cameraTransform.rotation = originalRotation * Quaternion.Euler(0f, 0f, randomRotation);

            // Reduce the timer
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // If the timer has expired, reset the camera's rotation
            shakeTimer = 0f;
            cameraTransform.rotation = originalRotation;
        }

        // Trigger the camera shake (e.g., on a specific event)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartShake();
        }
    }

    // Start the camera shake
    public void StartShake()
    {
        shakeTimer = shakeDuration;
    
    }
}