using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public string SceneName;
	private AudioSource AudioSource;
	public AudioClip click;
	public AudioClip negative;

	// Start is called before the first frame update
	void Start()
    {
		AudioSource = gameObject.GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene(SceneName);
		AudioSource.PlayOneShot(click);
    }

    public void options()
	{
		AudioSource.PlayOneShot(click);
	}

    public void quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
		AudioSource.PlayOneShot(negative);
	}
}
