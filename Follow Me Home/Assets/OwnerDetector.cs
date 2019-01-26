using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnerDetector : MonoBehaviour
{
    OwnerAI selfAIScript;
    private void Start()
    {
        selfAIScript = transform.parent.GetComponent<OwnerAI>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Dog")
        {
            selfAIScript.suspicionLevel+=0.1f;   
        }
    }
}
