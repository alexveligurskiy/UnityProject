using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControll : MonoBehaviour {
	public static LevelControll current;
	public UILabel CoinsLabel;
	public UILabel FruitsLabel;
	public UI2DSprite Health;
	public UI2DSprite Gems;
	Vector3 startingPosition;

	void Awake() {
		current = this;
	}
	public void setStartPosition(Vector3 pos) {
		this.startingPosition = pos;
	}
	public void onRabbitDeath(HeroRabbit rabbit) {
		//При смерті кролика повертаємо на початкову позицію
		rabbit.transform.position = this.startingPosition;
	}

}