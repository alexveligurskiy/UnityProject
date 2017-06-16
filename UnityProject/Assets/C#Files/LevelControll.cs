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
	int coins = 0;
	int fruits = 0;
	int gems = 3;
	//int life = 3;
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
	public void addCoins(int coin){
		this.coins += coin;
		string NumberOfCoins = coins.ToString();
        string zero = "";
		for (int i = 0; i < 4 - NumberOfCoins.Length; i++) {
	            zero += "0";
	        }
		zero += NumberOfCoins;
        CoinsLabel.text = zero;
	}

	public void addFruit(int fruit){
		this.fruits += fruit;
		FruitsLabel.text = fruits.ToString();
	}

	public void addGem(){
		Sprite[] Crystal = Resources.LoadAll<Sprite>("crystals");
        --gems;
        SpriteRenderer sr = Gems.gameObject.GetComponentsInChildren<SpriteRenderer>()[gems];
		sr.sprite = Crystal[gems];
	}
}