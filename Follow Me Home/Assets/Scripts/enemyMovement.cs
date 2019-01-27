using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class enemyMovement : MonoBehaviour
{

    private enum State
    {
        Chill,
        Suspicious,
        Stopping,
        Stopped,
        Resuming
    }


    public Transform waypointsParent;
    public Detector detector;
    public GameObject suspiciousIcon;
    public GameObject alertedIcon;
    public Menu_GameOver gameOver;
    public float smoothing = 0.1f;
    public GameObject doggo;
    public float degreesPerSecond = 720.0f;
    public float stopDuration = 1.0f;

    private NavMeshAgent agent;
    private int destination;
    private float remainingDistance = float.MaxValue;
    private State state = State.Chill;
    private float stopTime;

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

        switch (state)
        {
            case State.Chill:
                if (remainingDistance < 10.0f)
                {
                    SetState(State.Suspicious);
                }
                break;
            case State.Suspicious:
                if (remainingDistance < 0.1f)
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
                    }

                    SetState(State.Stopping);
                }
                break;
            case State.Stopping:
            {
                Quaternion toDoggo = Quaternion.LookRotation((doggo.transform.position - transform.position).normalized, Vector3.up);
                Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, toDoggo, Time.deltaTime * degreesPerSecond);
                if (Quaternion.Angle(transform.rotation, newRotation) > 5.0f)
                {
                    transform.rotation = newRotation;
                }
                else
                {
                    SetState(State.Stopped);
                }
                break;
            }
            case State.Stopped:
                if (((Time.time - stopTime) > stopDuration) && gameOver.isActiveAndEnabled == false)
                {
                    Debug.Log("Stopped");
                    if (detector.isHiding)
                    {
                        SetState(State.Resuming);
                    }
                    else
                    {
                        Debug.Log("I SEE YOU!");
                        gameOver.gameObject.SetActive(true);
                    }
                }
                break;
            case State.Resuming:
            {
                Quaternion toDoggo = Quaternion.LookRotation((doggo.transform.position - transform.position).normalized, Vector3.up);
                Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, toDoggo, Time.deltaTime * degreesPerSecond);
                if (Quaternion.Angle(transform.rotation, newRotation) > 5.0f)
                {
                    transform.rotation = newRotation;
                }
                else
                {
                    SetState(State.Chill);
                }
                break;
            }
        }
    }

    private void SetState(State newState)
    {
        switch (newState)
        {
            case State.Chill:
                suspiciousIcon.SetActive(false);
                alertedIcon.SetActive(false);
                agent.isStopped = false;
                state = State.Chill;
                break;
            case State.Suspicious:
                Debug.Log("Did I hear something?");
                suspiciousIcon.SetActive(true);
                alertedIcon.SetActive(false);
                agent.isStopped = false;
                state = State.Suspicious;
                break;
            case State.Stopping:
                suspiciousIcon.SetActive(true);
                alertedIcon.SetActive(false);
                agent.isStopped = true;
                state = State.Stopping;
                break;
            case State.Stopped:
                Debug.Log("Who goes there?");
                suspiciousIcon.SetActive(false);
                alertedIcon.SetActive(true);
                agent.isStopped = true;
                state = State.Stopped;
                stopTime = Time.time;
                break;
            case State.Resuming:
                Debug.Log("Must have been nothing.");
                suspiciousIcon.SetActive(false);
                alertedIcon.SetActive(false);
                agent.isStopped = true;
                state = State.Resuming;
                break;
        }
    }
}
