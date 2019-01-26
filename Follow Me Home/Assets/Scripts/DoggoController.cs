using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoController : MonoBehaviour
{
	public float staminaThreshold = 20.0f;
	public float staminaDiff = 0.5f;
	public float stamina = 10.0f;

	public float walkSpeed = 5.0f;
    public float sprintMultiplier = 2.0f;
    public KeyCode furtherKey = KeyCode.W;
    public KeyCode closerKey = KeyCode.S;
    public KeyCode stopKey = KeyCode.A;
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
    void FixedUpdate()
	{
		float speed = walkSpeed;
		if (Input.GetKey(stopKey) && stamina < staminaThreshold)
		{
			speed = 0.0f;
			stamina += staminaDiff;
			if (stamina > staminaThreshold)
			{
				stamina = staminaThreshold;
			}
		}
		if (Input.GetKey(sprintKey) && stamina > 0.0f)
		{
			speed *= sprintMultiplier;
			stamina -= staminaDiff;
			if (stamina < 0.0f)
			{
				stamina = 0.0f;
			}
		}

		Move(Time.fixedDeltaTime * speed, 0.0f, 0.0f);

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

    void OnCollisionEnter(Collision other)
    {
        // stop
        Debug.Log("sup");
    }
}
