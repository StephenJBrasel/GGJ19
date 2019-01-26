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
			controller.stamina += power;
			if (controller.stamina < 0.0f)
			{
				controller.stamina = 0.0f;
			}
			else if (controller.stamina > controller.staminaThreshold)
			{
				controller.stamina = controller.staminaThreshold;
			}
			item.SetActive(false);
			Debug.Log("Power up!");
		}
	}
}
