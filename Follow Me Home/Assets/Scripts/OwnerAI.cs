using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnerAI : MonoBehaviour
{
    bool isTurnningAround = false;
    [SerializeField]
    float[] triggerPoints;

    [SerializeField]
    GameObject dog;
    Detector dogDetectorScript;
    public float suspicionLevel;

    private void Start()
    {
        dogDetectorScript = GetComponent<Detector>();   
    }


    private void Update()
    {

        Debug.Log("Suspicion Level: " + suspicionLevel);
        if (suspicionLevel >= 10.0f)
        {
            isTurnningAround = true;
               suspicionLevel = 0.0f;
        }

        if (!isTurnningAround)
        {
            transform.Translate(2.0f * Time.deltaTime, 0, 0);
        }

        if (isTurnningAround)
        {
            if (dogDetectorScript.isHiding)
            {
                Debug.Log("Serached for dog but didn't see anything");
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SuspicionTrigger") && suspicionLevel<10.0f)
        {
            suspicionLevel = 10.0f;
        }
    }






}
