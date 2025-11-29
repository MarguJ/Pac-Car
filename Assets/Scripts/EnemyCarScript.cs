using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EnemyCarScript : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float pointReachedThreshold = 0.3f;
    [SerializeField] private float viewDistance = 10;
    
    private int currentPointIndex = 0;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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
        
        if (Physics.Raycast(enemyView, out hit, viewDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player Hit");
            }
        }
        else
        {
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
}
