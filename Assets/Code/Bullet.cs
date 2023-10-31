using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;   // Speed of the bullet
    public float lifetime = 10f; // Time until the bullet is destroyed
    public LayerMask enemyLayer; // Layer for enemies
    public GameObject explosionPrefab; // Reference to the explosion particle system prefab

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
        if (enemyLayer == (enemyLayer | (1 << other.gameObject.layer)))
        {
            // Instantiate the explosion at the enemy's position
            GameObject explosion = Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);
            ParticleSystem explosionEffect = explosion.GetComponent<ParticleSystem>();

            // Handle enemy hit by destroying it
            Destroy(other.gameObject);
            Destroy(explosion,2f);

            // Start a coroutine to pause and destroy the explosion after 2 seconds
            StartCoroutine(PauseAndDestroyExplosion(explosionEffect));
        }

        // Destroy the bullet on collision with any object
        Destroy(gameObject);
    }

    // Coroutine to pause and destroy the explosion after 2 seconds
    private IEnumerator PauseAndDestroyExplosion(ParticleSystem explosionEffect)
    {
        yield return new WaitForSeconds(2f);

        // Pause the explosion effect
        explosionEffect.Pause();

        // Wait for an additional 2 seconds
        yield return new WaitForSeconds(2f);

        // Destroy the explosion effect
        Destroy(explosionEffect.gameObject);
    }
}
