using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float carSpeed;
    public float carRotationSpeed; 
    public float maxVelocity;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
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
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }
}

