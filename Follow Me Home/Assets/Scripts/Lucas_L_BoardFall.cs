//Creator: Lucas LaMonica
//Date: 1/26/2019
//References Used: Unity Documentation- Quaternion.Lerp
//References Links: https://docs.unity3d.com/ScriptReference/Quaternion.Lerp.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucas_L_BoardFall : MonoBehaviour
{
    public float speed;
    public GameObject doggo;
    public bool isHit;
    public Animator anim;

	private DoggoController controller;
	
	void Start()
    {
		controller = doggo.GetComponent<DoggoController>();

    }

	private void OnCollisionEnter(Collision other)
	{
		int x = 1;
        if(other.collider.gameObject.tag == "Player")
        {
            isHit = true;
            if(controller.isSprinting == true)
            {
				//animation["BridgePlank : Rotation"].wrapMode = WrapMode.Once;
				//anim.wrapMode = WrapMode.Once;
				anim.SetTrigger("readyToFall");
                Debug.Log("Doggo is in collider");
            }
        }
    }

	private void OnTriggerStay(Collider other)
	{

	}
}
