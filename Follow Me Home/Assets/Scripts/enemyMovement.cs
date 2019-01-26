using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{

    public Transform[] waypoints;
    
    private NavMeshAgent agent;
    private int destination = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[destination].position);
        moveToNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            moveToNextWaypoint();
        }
        
    }

    private void moveToNextWaypoint()
    {
        // Set the way point for the NPC to move to.
        agent.SetDestination(waypoints[destination].position);

        // Set the next waypoint in the array.
        destination = ++destination % waypoints.Length;
        Debug.Log("Alerted");
        Debug.Log("Turn Around");

    }

}
