using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entries : MonoBehaviour {
	public string level;
	void OnTriggerEnter2D(Collider2D collider)
	{
		HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
		if (rabbit != null)
		{
			SceneManager.LoadScene(level);
		}


	}
}