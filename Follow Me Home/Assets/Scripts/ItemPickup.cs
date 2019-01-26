using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public float power;

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
			this.gameObject.SetActive(false);
			Debug.Log("Power up!");
		}
	}
}
