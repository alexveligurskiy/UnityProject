using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : Collectable{
	protected override void OnRabbitHit(HeroRabbit rabit){
		LevelControll.current.addFruit(1);
		this.CollectedHide();
	}
}