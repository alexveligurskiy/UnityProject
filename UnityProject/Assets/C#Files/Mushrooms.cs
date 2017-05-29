using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrooms : Collectable{
	protected override void OnRabbitHit(HeroRabbit rabit){
		rabit.addOneHealth();
		this.CollectedHide();
	}
}