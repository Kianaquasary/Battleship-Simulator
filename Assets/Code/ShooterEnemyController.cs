using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShooterEnemyController : MonoBehaviour
{
    public Transform target; // Target to move towards
    public float minSpeed = 2.0f; // Minimum movement speed
    public float maxSpeed = 5.0f; // Maximum movement speed
    public float shootingDistance = 20.0f; // Distance to start shooting
    public GameObject bulletPrefab; // Prefab of the bullet
    public Transform firePoint; // Point where bullets are fired from
    public float fireRate = 1.0f; // Fire rate in bullets per second

    private float currentSpeed;
    private float changeDirectionTimer = 2.0f;
    private float sineAmplitude = 2.0f;
    private float sineFrequency = 1.0f;
    private float lastFireTime;

    private void Start()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        InvokeRepeating("ChangeDirection", 0, changeDirectionTimer);
    }

    private void Update()
    {
        transform.LookAt(target);

        if (target == null)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget
            > shootingDistance)
        {
            MoveTowardsTarget();
        }
        else
        {
            // Stop moving and start shooting
            ShootAtTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // Calculate a new position with a sine movement
        float xOffset = Mathf.Sin(Time.time * sineFrequency) * sineAmplitude;
        Vector3 newPosition = new Vector3(target.position.x + xOffset, transform.position.y, target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * currentSpeed);
    }

    private void ChangeDirection()
    {
        // Change the speed and direction
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        sineAmplitude = Random.Range(1.0f, 3.0f);
        sineFrequency = Random.Range(0.5f, 1.5f);
    }

    private void ShootAtTarget()
    {
        if (Time.time - lastFireTime >= 1.0f / fireRate)
        {
            // Instantiate a bullet at the fire point
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Set the bullet's velocity to move towards the target
            Vector3 bulletDirection = (target.position - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = bulletDirection * bullet.GetComponent<EnemyBullet>().speed;

            lastFireTime = Time.time;
        }
    }
}
