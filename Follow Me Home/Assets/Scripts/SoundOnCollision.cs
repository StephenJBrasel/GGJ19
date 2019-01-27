using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{
	private AudioSource AudioSource;
	public AudioClip[] possibles;
	private AudioClip chosenClip;
	private Obstacle.Type ObstacleType;
    // Start is called before the first frame update
    void Start()
    {
		AudioSource = gameObject.GetComponent<AudioSource>();
		ObstacleType = gameObject.GetComponent<Obstacle>().GetObstacleType();
    }
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Player")
		{
			int randStart = 0;
			switch (ObstacleType)
			{
				case Obstacle.Type.BikeRack:
					randStart = 0;
					break;
				case Obstacle.Type.FireHydrant:
					randStart = 4;
					break;
				case Obstacle.Type.Scaffold:
					randStart = 8;
					break;
				case Obstacle.Type.ShopSign:
					randStart = 12;
					break;
				case Obstacle.Type.TrashCan:
					randStart = 16;
					break;
			}
			int choice = Random.Range(randStart, randStart + 3);
			chosenClip = possibles[choice];
			//AudioSource.pitch = Random.Range(-3, 3);
			//AudioSource.pitch += Random.value;
			AudioSource.clip = chosenClip;
			AudioSource.Play();
		}
	}
}
