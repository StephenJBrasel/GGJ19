using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
	private AudioSource AudioSource;
	public AudioClip music;
	// Start is called before the first frame update
	void Start()
    {
		AudioSource = gameObject.GetComponent<AudioSource>();
		AudioSource.loop = true;
		AudioSource.Play();
	}

	// Update is called once per frame
	void Update()
    {
	}
}
