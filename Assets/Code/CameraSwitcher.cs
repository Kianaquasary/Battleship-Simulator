using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject firstCameraObject;
    public GameObject secondCameraObject;

    public GameObject crosshair;
    public GameObject turret;

    private bool isFirstCameraActive = true;

    private void Start()
    {
        // Initially, enable the first camera and disable the second camera.
        firstCameraObject.SetActive(true);

        secondCameraObject.SetActive(false);
        crosshair.SetActive(false);
        turret.SetActive(true);
    }

    private void Update()
    {
        // Check if the "Shift" key is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            // Toggle the active state of the camera GameObjects
            isFirstCameraActive = !isFirstCameraActive;
            firstCameraObject.SetActive(isFirstCameraActive);
            secondCameraObject.SetActive(!isFirstCameraActive);
            crosshair.SetActive(!isFirstCameraActive);
            turret.SetActive(isFirstCameraActive);
        }
    }
}
