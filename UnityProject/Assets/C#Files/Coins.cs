using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable{
	protected override void OnRabbitHit(HeroRabbit rabit){
		LevelControll.current.addCoins(1);
		this.CollectedHide();
	}
}