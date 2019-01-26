using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStaminaSlider : MonoBehaviour
{
	public GameObject doggo;
	public Slider staminaBar;

	private DoggoController controller;

	private void Start()
	{
		controller = doggo.GetComponent<DoggoController>();
		staminaBar.maxValue = controller.staminaThreshold;
	}

	// Update is called once per frame
	void Update()
    {
		staminaBar.value = controller.stamina;
	}
}
