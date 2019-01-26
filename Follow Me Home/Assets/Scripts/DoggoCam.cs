using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoCam : MonoBehaviour
{
    public GameObject doggo;
    public float smoothTime = 0.1f;
    public float maxSpeed = 10.0f;

    private Vector3 offset = Vector3.zero;

    private Vector3 currentVelocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        offset = gameObject.transform.position - doggo.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = doggo.transform.position + offset;
        targetPosition = Vector3.SmoothDamp(gameObject.transform.position, targetPosition, ref currentVelocity, smoothTime, maxSpeed);
        gameObject.transform.position = targetPosition;
    }
}
