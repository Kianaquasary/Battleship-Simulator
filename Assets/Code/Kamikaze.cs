using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{

    public int damage = 30;
    public GameObject particleSystemPrefab; // Reference to the Particle System Prefab

    private void OnTriggerEnter(Collider other)
    {
        // Check for collision with other objects and handle accordingly
        if (other.CompareTag("Player"))
        {
            // Handle enemy hit
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Debug.Log("Hit");

            // Instantiate the Particle System Prefab at the collision point
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
        }

        // Destroy the Kamikaze enemy on collision with any object
        Destroy(gameObject);
    }
}
