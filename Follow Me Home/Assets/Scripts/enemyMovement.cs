﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{

    public Transform[] waypoints;
    public float speed;

    private bool toNextWaypoint = true;

    private int currentWaypoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (currentWaypoint < waypoints.Length)
        {
            moveToNextWaypoint();
        }
    }

    private void moveToNextWaypoint()
    {
        if (toNextWaypoint)
        {
            Vector3 nextWaypoint = waypoints[currentWaypoint].position;
            Vector3 direction = nextWaypoint - transform.position;
        }
    }
}
