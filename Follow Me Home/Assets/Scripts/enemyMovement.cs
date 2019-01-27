using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{

    public Transform[] waypoints;
    public Detector detector;

    private NavMeshAgent agent;
    private int destination;
    private bool isSearching = false;
    private bool isAlerted = false;
    private float remainingDistance = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[destination].position);
        destination = 0;
    }

    // Update is called once per frame
    void Update()
    {
        remainingDistance = waypoints[destination].position.x - agent.transform.position.x;
        if (!agent.pathPending && remainingDistance < 0.5f && !isAlerted)
        {
            moveToNextWaypoint();
        }

    }

    private void moveToNextWaypoint()
    {
        // Set the way point for the NPC to move to.
        if (destination < (waypoints.Length - 1))
        {
            destination++;
            agent.SetDestination(waypoints[destination].position);
        }

        // If enemy is at the last way point stop
        else if (destination == (waypoints.Length - 1))
        {
            Debug.Log("Destination: " + destination.ToString());
            Debug.Log("Waypoints Len: " + waypoints.Length.ToString());
            agent.isStopped = true;
        }

        if (destination % 2 == 0)
        {
            Debug.Log("Remaining Distance: " + remainingDistance.ToString());

            alerted();
        }
    }

    private void alerted()
    {
        isAlerted = true;
        Debug.Log("I am alerted!");
        turnAround();
    }

    private void turnAround()
    {
        agent.isStopped = true;
        if (!detector.isHiding)
        {
            Debug.Log("I SEE YOU!");
        }
        else
        {
            isAlerted = false;
            agent.isStopped = false;
            Debug.Log("Must have been nothing.");
        }
    }

}
