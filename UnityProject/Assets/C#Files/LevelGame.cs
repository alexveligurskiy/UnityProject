using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelGame : MonoBehaviour {
	public GameObject Window;

	public MyButton PauseButton;
	void Start () {
		
		PauseButton.signalOnClick.AddListener(this.onPause);
	}

	void onPause(){

		GameObject obj = GameObject.Find("UI Root").AddChild(this.Window);

		obj.transform.position = this.transform.position;
		obj.transform.position += new Vector3(-9.0f, -4.0f, 0.0f);

		PauseWindow Window = obj.GetComponent<PauseWindow>();


	}
}
