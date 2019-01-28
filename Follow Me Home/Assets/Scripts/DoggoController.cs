using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoggoController : MonoBehaviour
{
        public Animator animator;
        public float animationSpeed = 1.0f;

    [Header("Stamina")]
    public bool staminaEnabled = true;
	public float staminaThreshold = 40.0f;
	public float staminaDiff = 0.5f;
	public float stamina = 20.0f;

    [Header("Speed")]
	public float walkSpeed = 5.0f;
    public float sprintMultiplier = 2.0f;

    [Header("Input")]
    public KeyCode furtherKey = KeyCode.W;
    public KeyCode closerKey = KeyCode.S;
    public KeyCode stopKey = KeyCode.A;
    public KeyCode sprintKey = KeyCode.D;
    public KeyCode restartKey = KeyCode.R;

    [Header("Z Movement")]
    public float smoothTime = 0.1f;
    public float maxSpeed = 10.0f;
    public int maxZ = 4;
    public int minZ = 0;
    public Transform rotationTarget;


    [HideInInspector]
    public bool isSprinting = false;

    private float currentZVelocity = 0.0f;
    private int targetZ;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.RegisterKeyActivated(furtherKey, MoveFurther);
        InputManager.RegisterKeyActivated(closerKey, MoveCloser);
        InputManager.RegisterKeyActivated(restartKey, Restart);
        InputManager.RegisterKeyActive(stopKey, Dummy);
        InputManager.RegisterKeyActive(sprintKey, Dummy);
        targetZ = Mathf.RoundToInt(transform.position.z);
        stamina = staminaThreshold * 0.5f;
        initialRotation = rotationTarget.rotation;
        animator.speed = animationSpeed;
    }

    void Restart()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        animator.speed = animationSpeed;
        bool sprint = false;
        if (InputManager.IsKeyActive(stopKey))
        {
            if (!staminaEnabled || stamina < staminaThreshold)
            {
                xPerSecond = 0.0f;
                animator.speed = 0.0f;

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
                sprint = true;
                xPerSecond *= sprintMultiplier;
                animator.speed = animationSpeed * sprintMultiplier;

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
        isSprinting = sprint;

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
            rotationTarget.rotation = initialRotation * Quaternion.Euler(0.0f, angle, 0.0f);
        }
    }
}
