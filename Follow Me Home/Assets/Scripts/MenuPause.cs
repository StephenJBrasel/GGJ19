using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
	public bool GameIsPaused = false;
	public GameObject PauseMenuUI;
	private AudioSource AudioSource;
	public GameObject Menu;
	private AudioSource LevelMusic;
	public AudioClip click;

	private void Start()
	{
		LevelMusic = Menu.GetComponent<AudioSource>();
		LevelMusic.Pause();
		AudioSource = gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		AudioSource.PlayOneShot(click);
		LevelMusic.UnPause();
	}

	public void Pause()
	{
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
		AudioSource.PlayOneShot(click);
	}

	public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
		AudioSource.PlayOneShot(click);
		LevelMusic.Play();
	}

	public void LoadMenu()
	{
		Debug.Log("Loading...");
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
		AudioSource.PlayOneShot(click);
	}

	public void QuitGame()
	{
		Debug.Log("Quitting...");
		Application.Quit();
		AudioSource.PlayOneShot(click);
	}

}
