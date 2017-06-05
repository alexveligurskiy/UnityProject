using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControll : MonoBehaviour {
	public static LevelControll current;
	Vector3 startingPosition;
	int coins = 0;
	int fruits = 0;
	int gems = 0;
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
	}

	public void addFruit(int fruit){
		this.fruits += fruit;
	}

	public void addGem(int gem){
		this.gems += gem;
	}
}