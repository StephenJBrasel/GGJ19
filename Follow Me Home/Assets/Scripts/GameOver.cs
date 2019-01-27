using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
	public GameObject doggo;
	public GameObject owner;
	public Menu_GameOver gameOverMenu;
	public float maxDistance = 10;

	private float xPosDoggo;
	private float xPosOwner;
    // Start is called before the first frame update
    void Start()
    {
		xPosDoggo = doggo.transform.position.x;
		xPosOwner = owner.transform.position.x;
	}

    // Update is called once per frame
    void Update()
	{
		xPosDoggo = doggo.transform.position.x;
		xPosOwner = owner.transform.position.x;
		if (Mathf.Abs(xPosOwner - xPosDoggo) > maxDistance)
		{
			gameOverMenu.gameObject.SetActive(true);
		}
    }
}
