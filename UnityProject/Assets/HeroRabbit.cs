﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
	public float speed = 1;
	public float JumpTime = 0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;


	//float diff = Time.deltaTime;
	bool isGrounded = false;
	bool JumpActive = false;

	Transform heroParent = null;
	Rigidbody2D myBody = null;
	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D> ();
		LevelControll.current.setStartPosition (transform.position);
		this.heroParent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate (){
		float value = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}

		Animator animator = GetComponent<Animator> ();
		if(Mathf.Abs(value) > 0) {
			animator.SetBool ("run", true);
		} else {
			animator.SetBool ("run", false);
		}
		if(this.isGrounded) {
			animator.SetBool ("jump", false);
		} else {
			animator.SetBool ("jump", true);
		}
		   


		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if(value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}
		//class HeroRabit, void FixedUpdate()
		Vector3 from = transform.position + Vector3.up * 0.3f;
		Vector3 to = transform.position + Vector3.down * 0.1f;
		int layer_id = 1 << LayerMask.NameToLayer ("Ground");
		//Перевіряємо чи проходить лінія через Collider з шаром Ground
		RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
		if(hit) {
			isGrounded = true;
			if (hit.transform != null
				&& hit.transform.GetComponent<MovingPlatform>() != null)
			{
				//Приліпаємо до платформи
				this.transform.parent = hit.transform;
			}
		} else {
			isGrounded = false;
			this.transform.parent = this.heroParent;
		}
		//Намалювати лінію (для розробника)
		Debug.DrawLine (from, to, Color.red);
		if(Input.GetButtonDown("Jump") && isGrounded) {
			this.JumpActive = true;
		}
		if (this.JumpActive){
			//Якщо кнопку ще тримають
			if (Input.GetButton("Jump")){
				this.JumpTime += Time.deltaTime;
				if (this.JumpTime < this.MaxJumpTime)
				{
					Vector2 vel = myBody.velocity;
					vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
					myBody.velocity = vel;
				}
			}else{
				this.JumpActive = false;
				this.JumpTime = 0;
			}
		}
		if (this.isGrounded){
			animator.SetBool("jump", false);
		}
		else{
			animator.SetBool("jump", true);
		}
	}
}
