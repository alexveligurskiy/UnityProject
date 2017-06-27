using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseWindow : MonoBehaviour {

	public GameObject Window;
	public MyButton HomeButton;
	public MyButton MusicButton;
	public MyButton CloseButton;
	public MyButton Background;

	void Start () {
		Background.signalOnClick.AddListener (this.onClose);
		HomeButton.signalOnClick.AddListener (this.onPlay);
		MusicButton.signalOnClick.AddListener (this.onMusic);
		CloseButton.signalOnClick.AddListener (this.onClose);


	}

	void onPlay() {
		//Do something
		//SceneManager.LoadScene("StartScene");

	}
	void onMusic(){

	}
	void onClose(){
		Time.timeScale = 1;
		Destroy(this.gameObject);
	}

}
