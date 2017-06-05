using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	bool hide = false;
	protected virtual void OnRabbitHit(HeroRabbit rabbit){
		
	}
	void OnTriggerEnter2D(Collider2D collider){
		if (!this.hide){
			HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
			if (rabbit != null){
				this.OnRabbitHit(rabbit);
			}
		}
	}
	public void CollectedHide(){
		Destroy(this.gameObject);
		hide = true;
	}

}