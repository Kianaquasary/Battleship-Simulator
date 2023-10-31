using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 5f;     // Speed at which the ship moves forward
    public float rotationSpeed = 2f;    // Speed at which the ship rotates
    public float acceleration = 0.5f;   // Acceleration of the ship's speed
    public float deceleration = 0.5f;   // Deceleration of the ship's speed
    public float maxSpeed = 10f;        // Maximum speed of the ship
    public float rotationDelay = 0.5f;  // Delay after releasing rotation keys before the ship stops rotating

    private float currentSpeed = 0f;    // Current speed of the ship
    private float currentRotation = 0f; // Current rotation angle of the ship
    private float rotationTimer = 0f;   // Timer to delay stopping rotation


    // Add an AudioSource component in the Unity Editor and assign an engine sound to this field.
    public AudioSource engineSound;

    public float targetEngineVolume = 0f;
    private float currentEngineVolume = 0f;

    private void Update()
    {
        HandleInput();
        MoveShip();

        UpdateEngineSound();
    }


    private void UpdateEngineSound()
    {
        // Gradually adjust the engine sound volume based on the target volume.
        if (engineSound)
        {
            currentEngineVolume = Mathf.Lerp(currentEngineVolume, targetEngineVolume, Time.deltaTime);
            engineSound.volume = currentEngineVolume;
        }
    }

    private void HandleInput()
    {
        if(currentSpeed <= 0)
        {
            StopEngineSound();
        }


        // Forward movement
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            PlayEngineSound();
        }

        // Rotation
        if (Input.GetKey(KeyCode.A))
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
            rotationTimer = 0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentRotation += rotationSpeed * Time.deltaTime;
            rotationTimer = 0f;
        }
        else
        {
            // Delay stopping rotation after releasing rotation keys
            //rotationTimer = 0f;
            //if (rotationTimer >= rotationDelay)
            //{
                float targetRotation = currentRotation >= 0f ? currentRotation + 0.5f : currentRotation - 0.5f;
                currentRotation = Mathf.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
           // }
        }

        // Backward movement
        if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.Max(currentSpeed - deceleration * Time.deltaTime, -maxSpeed);
            PlayEngineSound();
        }
    }

    private void MoveShip()
    {
        // Apply rotation to ship
        transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

        // Apply forward/backward movement to ship
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }


    private void PlayEngineSound()
    {
        if (engineSound)
        {
            // Set the target engine volume to maximum (e.g., 1) when accelerating.
            targetEngineVolume = 1f;
            if (!engineSound.isPlaying)
            {
                engineSound.Play();
            }
        }
    }

    private void StopEngineSound()
    {
        if (engineSound)
        {
            // Set the target engine volume to minimum (e.g., 0) when not accelerating.
            targetEngineVolume = 0f;
        }
    }
}
