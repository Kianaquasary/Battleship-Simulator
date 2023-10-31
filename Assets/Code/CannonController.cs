using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CannonController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit))
        {
            // Check if the ray hit an enemy or other target
            // Apply damage or perform other actions
        }

        // Create a bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}


