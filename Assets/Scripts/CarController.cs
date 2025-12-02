using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float carSpeed;
    public float carRotationSpeed;
    public float carGravity;
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
            float moveVertical = Input.GetAxis("Vertical");
            if (rb.velocity.magnitude < maxVelocity)
            {
                rb.AddForce(moveVertical * carSpeed * transform.forward, ForceMode.Force);
            }
            
            rb.AddForce(0, -carGravity, 0 ,ForceMode.Force);
        
            float moveHorizontal = Input.GetAxis("Horizontal");
        
            float speedFactor = rb.velocity.magnitude / maxVelocity;

            if (moveVertical < 0)
            {
                moveHorizontal *= -1;
            }
            else if (moveHorizontal == 0)
            {
                moveHorizontal = 0;
            }
            
            float rotationAmount = carRotationSpeed * speedFactor * moveHorizontal;
        
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotationAmount, 0f));

            if (gameObject.transform.position.y < -30)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}

