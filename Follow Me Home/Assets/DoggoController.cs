using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoController : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    public KeyCode furtherKey = KeyCode.W;
    public KeyCode closerKey = KeyCode.S;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float smoothTime = 0.1f;
    public float maxSpeed = 10.0f;


    public float maxZ = 4.0f;
    public float minZ = 0.0f;

    private float currentZVelocity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetKey(sprintKey) ? sprintSpeed : walkSpeed;

        Move(Time.deltaTime * speed, 0.0f, 0.0f);

        if (Input.GetKeyDown(furtherKey))
        {
            Move(0.0f, 0.0f, 1.0f);
        }

        if (Input.GetKeyDown(closerKey))
        {
            Move(0.0f, 0.0f, -1.0f);
        }
    }

    void Move(Vector3 delta)
    {
        Move(delta.x, delta.y, delta.z);
    }

    void Move(float deltaX, float deltaY, float deltaZ)
    {
        Vector3 newPosition = new Vector3(gameObject.transform.position.x + deltaX, gameObject.transform.position.y + deltaY, gameObject.transform.position.z + deltaZ);
        newPosition = new Vector3(newPosition.x, newPosition.y, Mathf.Clamp(newPosition.z, minZ, maxZ));
        //newPosition.z = Mathf.SmoothDamp(gameObject.transform.position.z, newPosition.z, ref currentZVelocity, smoothTime, maxSpeed);
        gameObject.transform.position = newPosition;
    }
}
