using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public Vector3 MoveBy;
	public float Speed = 2f;
	public float WaitTime = 0.5f;

	public Vector3 pointA;
	public Vector3 pointB;
	// Use this for initialization
	void Start () {
		this.pointA = this.transform.position;
		this.pointB = this.pointA + this.pointB;
	}
	bool isArrived(Vector3 pos, Vector3 target){
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance (pos, target) < 0.02f;
	}

	// Update is called once per frame
	void Update () {
//		Vector3 target;
//
////		if (is_moving_A) {
//			target = this.pointA;
//		} else {
//			target = this.pointB;
//		}	
//		Vector3 my_pos = this.transform.position;
//		if(isArrived(target, my_pos)){
////			is_moving_A = !is_moving_A;
//		} else {
//			Vector3 destination = target - my_pos;
//		}
	}
}
