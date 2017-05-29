using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : Collectable{
	protected override void OnRabbitHit(HeroRabbit rabit){
		LevelControll.current.addGem(1);
		this.CollectedHide();
	}
}