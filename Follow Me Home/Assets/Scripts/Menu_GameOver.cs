using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_GameOver : MonoBehaviour
{
	private AudioSource AudioSource;
	public AudioClip click;
	public AudioClip[] GameOver;

	private void Start()
	{
		Time.timeScale = 0f;
		AudioSource = gameObject.GetComponent<AudioSource>();
		int choice = Random.Range(0, 2);
		AudioSource.PlayOneShot(GameOver[choice]);
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Time.timeScale = 1f;
		AudioSource.PlayOneShot(click);
	}
}
