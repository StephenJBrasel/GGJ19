using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoController : MonoBehaviour
{
    [Header("Stamina")]
    public bool staminaEnabled = true;
	public float staminaThreshold = 20.0f;
	public float staminaDiff = 0.5f;
	public float stamina = 10.0f;

    [Header("Speed")]
	public float walkSpeed = 5.0f;
    public float sprintMultiplier = 2.0f;

    [Header("Input")]
    public KeyCode furtherKey = KeyCode.W;
    public KeyCode closerKey = KeyCode.S;
    public KeyCode stopKey = KeyCode.A;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Z Movement")]
    public float smoothTime = 0.1f;
    public float maxSpeed = 10.0f;
    public int maxZ = 4;
    public int minZ = 0;
    public Transform rotationTarget;

    private float currentZVelocity = 0.0f;
    private int targetZ;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.RegisterKeyActivated(furtherKey, MoveFurther);
        InputManager.RegisterKeyActivated(closerKey, MoveCloser);
        InputManager.RegisterKeyActive(stopKey, Dummy);
        InputManager.RegisterKeyActive(sprintKey, Dummy);
        targetZ = Mathf.RoundToInt(transform.position.z);
    }

    void Dummy(){}

    void MoveFurther()
    {
        if (targetZ < maxZ)
        {
            ++targetZ;
        }
    }

    void MoveCloser()
    {
        if (targetZ > minZ)
        {
            --targetZ;
        }
    }

    // Update is called once per frame
    void Update()
	{
        float deltaX = 0.0f;
        float xPerSecond = walkSpeed;
        if (InputManager.IsKeyActive(stopKey))
        {
            if (!staminaEnabled || stamina < staminaThreshold)
            {
                xPerSecond = 0.0f;

                if (staminaEnabled)
                {
                    stamina += staminaDiff;
                    if (stamina > staminaThreshold)
                    {
                        stamina = staminaThreshold;
                    }
                }
            }
        }
        else if (InputManager.IsKeyActive(sprintKey))
        {
            if (!staminaEnabled || stamina > 0.0f)
            {
                xPerSecond *= sprintMultiplier;

                if (staminaEnabled)
                {
                    stamina -= staminaDiff;
                    if (stamina < 0.0f)
                    {
                        stamina = 0.0f;
                    }
                }
            }
        }

        deltaX = Time.deltaTime * xPerSecond;

        float newZ = Mathf.SmoothDamp(transform.position.z, targetZ, ref currentZVelocity, smoothTime, maxSpeed);
        Vector3 previousPosition = transform.position;
        transform.position = new Vector3(transform.position.x + deltaX, transform.position.y, newZ);

        if (rotationTarget != null)
        {
            Vector3 deltaPosition = transform.position - previousPosition;
            float angle = Vector3.Angle(Vector3.right, deltaPosition);
            if (deltaPosition.z > 0.0f)
            {
                angle *= -1.0f;
            }
            rotationTarget.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
    }
}
