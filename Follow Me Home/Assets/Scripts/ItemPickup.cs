using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	[SerializeField]
	public float power;
	public GameObject item;

	private void Start()
	{
		item = GetComponent<GameObject>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			DoggoController controller = other.GetComponent<DoggoController>();
			controller.walkSpeed += power;
			item.SetActive(false);
		}
	}
}
