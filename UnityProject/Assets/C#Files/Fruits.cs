using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : Collectable{
	protected override void OnRabbitHit(HeroRabbit rabbit){
		LevelControll.current.addFruit(1);
		this.CollectedHide();
	}
}