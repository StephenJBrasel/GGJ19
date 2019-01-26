using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	public string sceneName;

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player")
		{
			PlayerPrefs.SetInt(sceneName, 1);
			UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
		}
	}

}