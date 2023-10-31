using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatController : MonoBehaviour
{
    public Transform target;
    public float speedMin = 3.0f;
    public float speedMax = 6.0f;
    public float rotationSpeed = 5.0f;
    public float sineFrequency = 2.0f;
    public float sineAmplitude = 1.0f;
    public GameObject particleSystemPrefab; // Reference to the Particle System Prefab
    public AudioSource explosionSound; // Reference to the explosion sound AudioSource

    private float currentSpeed;
    private Vector3 initialPosition;
    public int damage = 30;

    void Start()
    {
        initialPosition = transform.position;
        currentSpeed = Random.Range(speedMin, speedMax);
    }

    void Update()
    {
        // Calculate the sine movement
        float time = Time.time;
        float sineOffset = Mathf.Sin(time * sineFrequency) * sineAmplitude;
        Vector3 targetPosition = target.position + new Vector3(sineOffset, 0, 0);

        // Calculate rotation to look at the target
        Vector3 direction = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Move towards the target using Lerp
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * currentSpeed);

        // Rotate smoothly to face the target
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        // Check the distance between the enemy and the target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // If the distance is less than or equal to 5 meters, play the explosion sound
        if (distanceToTarget <= 40.0f)
        {
            Debug.Log("Enemy is close to the target (within 5 meters)!");
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
                if (explosionSound != null)
                {
                    explosionSound.Play();
                }
                Destroy(gameObject);
            }
        }
    }

}
