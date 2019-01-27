//Creator: Lucas LaMonica
//Date: 1/26/2019
//References Used: Unity Documentation- Quaternion.Lerp
//References Links: https://docs.unity3d.com/ScriptReference/Quaternion.Lerp.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucas_L_BoardFall : MonoBehaviour
{

    //public Transform currentAngle;
    //public Transform newAngle;

    public float speed;

    public GameObject doggo;
    public DoggoController controller;

    public bool isHit;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //currentAngle = transform.eulerAngles;

        //controller = GameObject.FindObjectofType<DoggoController>();

    }

    // Update is called once per frame
    void Update()
    {
        /*currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, newAngle.x, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.y, newAngle.y, Time.deltaTime),
            Mathf.LerpAngle(currentAngle.z, newAngle.z, Time.deltaTime));*/

        if (isHit == true)
        {
            //transform.rotation = Quaternion.Lerp(currentAngle.rotation, newAngle.rotation, Time.time * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DoggoController>())
        {
            isHit = true;
            if(controller.isSprinting == true)
            {
                anim.SetTrigger("readyToFall");
                Debug.Log("Doggo is in collider");
            }
        }
    }
}
