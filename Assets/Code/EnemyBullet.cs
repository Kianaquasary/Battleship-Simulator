using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;   // Speed of the bullet
    public float lifetime = 10f; // Time until the bullet is destroyed
    public int damage = 10;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        // Destroy the bullet after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for collision with other objects and handle accordingly
        if (other.CompareTag("Player"))
        {
            // Handle enemy hit
            // Destroy(other.gameObject);
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Debug.Log("Hit");
        }

        // Destroy the bullet on collision with any object
        Destroy(gameObject);
    }
}
