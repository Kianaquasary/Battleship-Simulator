using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public GameObject Camera;
    public GameObject gameOver;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        Camera.SetActive(false);
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            if (playerHealth.currentHealth == 0)
            {
                Camera.SetActive(true);
                gameOver.SetActive(true);
                Application.Quit();
            }
           


        }

    }
}
