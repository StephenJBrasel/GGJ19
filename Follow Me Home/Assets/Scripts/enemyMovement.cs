using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{

    private enum AlertLevel
    {
        Chill,
        Suspicious,
        Alerted
    }

    public Transform waypointsParent;
    public Detector detector;
    public GameObject suspiciousIcon;
    public GameObject alertedIcon;

    private NavMeshAgent agent;
    private int destination;
    private bool isSearching = false;
    private bool isAlerted = false;
    private float remainingDistance = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypointsParent.GetChild(destination).position);
        destination = 0;
    }

    // Update is called once per frame
    void Update()
    {
        remainingDistance = waypointsParent.GetChild(destination).position.x - agent.transform.position.x;
        if (!agent.pathPending && !isAlerted)
        {
            if (remainingDistance < 0.25f)
            {
                moveToNextWaypoint();
            }
            else if (remainingDistance < 10.0f)
            {
                SetAlertLevel(AlertLevel.Suspicious);
            }
        }

    }

    private void SetAlertLevel(AlertLevel alertLevel)
    {
        switch (alertLevel)
        {
            case AlertLevel.Chill:
            suspiciousIcon.SetActive(false);
            alertedIcon.SetActive(false);
            break;
            case AlertLevel.Suspicious:
            suspiciousIcon.SetActive(true);
            alertedIcon.SetActive(false);
            break;
            case AlertLevel.Alerted:
            suspiciousIcon.SetActive(false);
            alertedIcon.SetActive(true);
            break;
        }
    }

    private void moveToNextWaypoint()
    {
        // Set the way point for the NPC to move to.
        if (destination < (waypointsParent.childCount - 1))
        {
            destination++;
            agent.SetDestination(waypointsParent.GetChild(destination).position);
        }

        // If enemy is at the last way point stop
        else if (destination == (waypointsParent.childCount - 1))
        {
            Debug.Log("Destination: " + destination.ToString());
            Debug.Log("Waypoints Len: " + waypointsParent.childCount.ToString());
            agent.isStopped = true;
        }

        //if (destination % 2 == 0)
        {
            Debug.Log("Remaining Distance: " + remainingDistance.ToString());

            alerted();
        }
    }

    private void alerted()
    {
        isAlerted = true;
        SetAlertLevel(AlertLevel.Alerted);
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
            SetAlertLevel(AlertLevel.Chill);
            agent.isStopped = false;
            Debug.Log("Must have been nothing.");
        }
    }

}
