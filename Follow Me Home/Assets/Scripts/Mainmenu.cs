using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
		PlayerPrefs.SetInt(SceneName, 1);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play(string scene)
    {
		if (PlayerPrefs.GetInt(scene) == 1)
		{
			SceneManager.LoadScene(SceneName);
		}
		else
		{
			Debug.Log("Can't play this level.");
		}
    }

    void options()
    {
       
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
