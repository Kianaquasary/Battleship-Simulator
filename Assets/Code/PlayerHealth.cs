using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    public int currentHealth;  // Current health of the player

    public GameObject explosionPrefab; // Prefab for the explosion particle system
    public AudioClip explosionSound;  // Sound to play when the player dies
    public float destroyDelay = 2.0f;  // Delay before destroying the player GameObject

    private AudioSource audioSource;

    public GameObject gameOver;
    public GameObject gameOverCamera;

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent < AudioSource>();

        //gameOver.SetActive(false);
    }

    // Function to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the player is dead
        if (currentHealth <= 0)
        {
            Die(); // Call a function to handle the player's death
        }
    }

    // Function to handle player's death
    private void Die()
    {
        // Instantiate the explosion particle system
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Play the explosion sound
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        // Destroy the player GameObject after a delay
        Destroy(gameObject, destroyDelay);

        // You can add additional logic here, such as respawning or ending the game
        Debug.Log("Player has died.");

       // gameOver.SetActive(true);
    }
}

