using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float carSpeed;
    public float carRotationSpeed; 
    public float maxVelocity;

    private Rigidbody rb;
    private bool controlsLocked = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!controlsLocked)
        {
            if (rb.velocity.magnitude < maxVelocity)
            {
                float moveVertical = Input.GetAxis("Vertical");
                rb.AddForce(moveVertical * carSpeed * transform.forward, ForceMode.Force);
            }
        
            float moveHorizontal = Input.GetAxis("Horizontal");
        
            float speedFactor = rb.velocity.magnitude / maxVelocity;
        
            float rotationAmount = moveHorizontal * carRotationSpeed * speedFactor;
        
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotationAmount, 0f));

            if (gameObject.transform.position.y < -30)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}

