using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoCam : MonoBehaviour
{
    public GameObject doggo;
    public float smoothTime = 0.1f;
    public float maxSpeed = 10.0f;

    private float xOffset = 0.0f;
    private float currentXVelocity = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        xOffset = transform.position.x - doggo.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.SmoothDamp(transform.position.x, doggo.transform.position.x + xOffset, ref currentXVelocity, smoothTime, maxSpeed);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
