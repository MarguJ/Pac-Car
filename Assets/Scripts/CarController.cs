using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed;
    
    private Vector3 moveForce;
    void Update()
    {
        moveForce += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime * -1;
        transform.position += moveForce * Time.deltaTime;
    }
}
