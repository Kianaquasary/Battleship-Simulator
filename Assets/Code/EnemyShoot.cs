using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShoot : MonoBehaviour
{
    public Transform target; // Target to move towards
 
    public float shootingDistance = 20.0f; // Distance to start shooting
    public GameObject bulletPrefab; // Prefab of the bullet
    public Transform firePoint; // Point where bullets are fired from
    public float fireRate = 1.0f; // Fire rate in bullets per second


    private float lastFireTime;


    private void Update()
    {
        transform.LookAt(target);

        if (target == null)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget
            > shootingDistance)
        {
            //MoveTowardsTarget();
        }
        else
        {
            // Stop moving and start shooting
            ShootAtTarget();
        }
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
