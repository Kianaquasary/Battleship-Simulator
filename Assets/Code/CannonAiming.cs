using UnityEngine;
using System.Collections;

public class CannonAiming : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform[] bulletSpawnPoints; // An array of bullet spawn points
    public float fireRate = 0.2f;
    private float nextFireTime = 0.0f;

    private float nextPowerFireTime = 0.0f;
    private float dealy = 10.0f;

    public float sensitivityX = 2.0f;
    public float sensitivityY = 2.0f;
    public float minimumX = -90.0f;
    public float maximumX = 90.0f;
    public float minimumY = -90.0f;
    public float maximumY = 90.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private int currentSpawnPointIndex = 0; // Keeps track of the current spawn point


    public Transform crosshair;
    public AudioSource[] shootingSounds; // Assign audio sources for each spawn point in the Unity Inspector
    public ParticleSystem[] impactEffectPrefabs; // Assign impact effect prefabs for each spawn point in the Unity Inspector

    void Start()
    {
        // Set the initial rotation to look at 180 degrees on the Y-axis
        rotationX = 0.0f; // Leave the X-axis rotation as it is
        rotationY = 0.0f; // Set the Y-axis rotation to 180 degrees

        // Apply the initial rotation to the camera
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }


    void Update()
    {
        // Check for firing
        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Fire();
        }

        if (Input.GetButtonDown("Fire1") && Time.time > nextPowerFireTime)
        {
            nextPowerFireTime = Time.time + dealy;
            PoweFire1();

        }

        // Camera rotation based on input axes
        rotationY = Mathf.Clamp(rotationY + Input.GetAxis("Mouse X") * sensitivityX, minimumY, maximumY);
        rotationX -= Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

        //UpdateCrosshairPosition();
    }

    void UpdateCrosshairPosition()
    {
        // Get the screen center position
       // Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Set the crosshair's position to the screen center
       // transform.position = screenCenter;
    }

    void Fire()
    {
        if (bulletPrefab && bulletSpawnPoints.Length > 0)
        {
            // Get the current spawn point
            Transform currentSpawnPoint = bulletSpawnPoints[currentSpawnPointIndex];

            // Create and shoot a bullet from the current spawn point
            GameObject bullet = Instantiate(bulletPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation);
            Rigidbody rb = bullet.GetComponent <Rigidbody>();
            if (rb)
            {
                rb.AddForce(currentSpawnPoint.forward, ForceMode.Impulse);
            }


            shootingSounds[0].Play();
            ParticleSystem impactEffect = Instantiate(impactEffectPrefabs[0], currentSpawnPoint.position, currentSpawnPoint.rotation);
            Destroy(impactEffect.gameObject, 2.0f); // Destroy the impact effect after 2 seconds

            // Move to the next spawn point (cycling back to the first if at the end)
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % bulletSpawnPoints.Length;
        }
    }



    void PoweFire1()
    {
        if (bulletPrefab && bulletSpawnPoints.Length > 0)
        {
            // Get the current spawn point
            Transform currentSpawnPoint = bulletSpawnPoints[0];

            // Create and shoot a bullet from the current spawn point
            GameObject bullet = Instantiate(bulletPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForce(currentSpawnPoint.forward, ForceMode.Impulse);
            }


    
                shootingSounds[1].Play();
                ParticleSystem impactEffect = Instantiate(impactEffectPrefabs[1], currentSpawnPoint.position, currentSpawnPoint.rotation);
                Destroy(impactEffect.gameObject, 2.0f); // Destroy the impact effect after 2 seconds
            

        }

        StartCoroutine(DelayedAction(0.2f, () =>
        {
            if (bulletPrefab && bulletSpawnPoints.Length > 0)
            {
                // Get the current spawn point
                Transform currentSpawnPoint = bulletSpawnPoints[1];

                // Create and shoot a bullet from the current spawn point
                GameObject bullet = Instantiate(bulletPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForce(currentSpawnPoint.forward, ForceMode.Impulse);
                }


                shootingSounds[2].Play();
                ParticleSystem impactEffect = Instantiate(impactEffectPrefabs[2], currentSpawnPoint.position, currentSpawnPoint.rotation);
                Destroy(impactEffect.gameObject, 2.0f); // Destroy the impact effect after 2 seconds

            }

        }));





        StartCoroutine(DelayedAction(0.3f, () =>
        {
            if (bulletPrefab && bulletSpawnPoints.Length > 0)
            {
                // Get the current spawn point
                Transform currentSpawnPoint = bulletSpawnPoints[2];

                // Create and shoot a bullet from the current spawn point
                GameObject bullet = Instantiate(bulletPrefab, currentSpawnPoint.position, currentSpawnPoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForce(currentSpawnPoint.forward, ForceMode.Impulse);
                }

            }
        }));



    }


    // Coroutine for delaying an action
    private IEnumerator DelayedAction(float delayTime, System.Action action)
    {
        yield return new WaitForSeconds(delayTime);
        action.Invoke();
    }


}
