using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour {
	public GameObject Window;
	public MyButton StartButton;
	public MyButton PauseButton;
	void Start () {
		StartButton.signalOnClick.AddListener (this.onPlay);
		PauseButton.signalOnClick.AddListener(this.onPause);
	}
	void onPlay() {
		//Do something
		SceneManager.LoadScene("LevelChooseScene");
	}
	void onPause(){

		GameObject obj = GameObject.Find("UI Root").AddChild(this.Window);

		obj.transform.position = this.transform.position;
		obj.transform.position += new Vector3(-9.0f, 5.0f, 0.0f);

		PauseWindow Window = obj.GetComponent<PauseWindow>();


	}
}
