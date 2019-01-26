using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
	public Text textobject;

	public void play()
	{
		if (PlayerPrefs.GetInt(textobject.text) == 1)
		{
			SceneManager.LoadScene(textobject.text);
		}
		else
		{
			Debug.Log("Can't play" + textobject.text);
		}
	}
}
