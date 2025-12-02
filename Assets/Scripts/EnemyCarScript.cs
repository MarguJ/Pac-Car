using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class EnemyCarScript : MonoBehaviour
{
    [Header("General Stats")]
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float pointReachedThreshold = 0.3f;
    [SerializeField] private float viewDistance = 10;
    [SerializeField] private float maxChaseRange = 60;

    [Header("Patrol stats")]
    [SerializeField] private float patrolSpeed = 15;
    [SerializeField] private float patrolSteer = 120;
    [SerializeField] private float patrolAcceleration = 10;
    
    [Header("Chase Stats")]
    [SerializeField] private float chaseSpeed = 20;
    [SerializeField] private float chaseSteer = 400;
    [SerializeField] private float chaseAcceleration = 40;
    [SerializeField] private float crashForce = 10;
    
    private Transform player;
    private int currentPointIndex = 0;
    private NavMeshAgent agent;
    private bool targeted;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
    }
    
    void Update()
    {
        RaycastHit hit;
        Ray enemyView = new Ray(transform.position, transform.position + transform.forward * viewDistance);
        
        Debug.DrawLine(transform.position, transform.position + transform.forward * viewDistance, Color.white);
        
        if (Physics.Raycast(enemyView, out hit, viewDistance) && !targeted)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player found");
                targeted = true;
                agent.SetDestination(player.position);
            }
        }
        else if (targeted)
        {
            float playerDistance = Vector3.Distance(transform.position, player.position);

            if (playerDistance < maxChaseRange)
            {
                agent.speed = chaseSpeed;
                agent.angularSpeed = chaseSteer;
                agent.acceleration = chaseAcceleration;
                agent.autoBraking = false;
                agent.SetDestination(player.position);
            }
            else
            {
                targeted = false;
            }
        }
        else
        {
            agent.speed = patrolSpeed;
            agent.angularSpeed = patrolSteer;
            agent.acceleration = patrolAcceleration;
            agent.autoBraking = true;
            if (patrolPoints.Length > 0)
            {
                if (!agent.pathPending && agent.remainingDistance <= pointReachedThreshold)
                {
                    currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
                    agent.SetDestination(patrolPoints[currentPointIndex].position);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Shock());
        }
    }

    private IEnumerator Shock()
    {
        yield return new WaitForSecondsRealtime(1);
        targeted = true;
    }
}
