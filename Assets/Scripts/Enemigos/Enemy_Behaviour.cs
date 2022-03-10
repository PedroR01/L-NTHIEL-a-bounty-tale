using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Behaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private Enemy enemyPatrolActivation;
    private int indexWayPoints;
    private Vector3 destination;

    private float standTimer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        standTimer = 4f;
        UpdateWayPoints();
    }

    private void Update()
    {
        if (enemyPatrolActivation.patrol)
            Patrol();
    }

    private void UpdateWayPoints()
    {
        destination = wayPoints[indexWayPoints].position;
        agent.SetDestination(destination);
    }

    private void IterateWayPoints()
    {
        indexWayPoints++;
        if (indexWayPoints == wayPoints.Length)
        {
            indexWayPoints = 0;
        }
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, destination) < 1)
        {
            standTimer -= Time.deltaTime;
            enemyPatrolActivation.stand = true;
            if (standTimer <= 0)
            {
                UpdateWayPoints();
                IterateWayPoints();
                enemyPatrolActivation.stand = false;
                standTimer = 4;
            }
        }
    }
}