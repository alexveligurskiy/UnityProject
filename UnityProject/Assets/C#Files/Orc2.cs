﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc2 : MonoBehaviour {

	public float speed = 1.0f;
	public Vector3 moveBy = Vector3.one;
	public float seeOn = 10.0f;
	public float carrotPeriod = 3.0f;

	public enum Mode
	{
		GoToA,
		GoToB,
		StandartAttack,
		CarrotAttack,
		Die
	}

	Rigidbody2D myBody = null;

	Vector3 pointA;
	Vector3 pointB;
	public Mode currentMode = Mode.GoToB;


	void Start()
	{
		this.myBody = this.GetComponent<Rigidbody2D>();
		this.pointA = this.transform.position;
		this.timeBefore = this.carrotPeriod;
		moveBy.y = 0;
		moveBy.z = 0;
		this.pointB = pointA + moveBy;

	}



	void FixedUpdate()
	{
		setMode();
		run();
		carrotAttack();
		StartCoroutine(die());

	}

	private float timeBefore;
	private void carrotAttack()
	{
		if (currentMode == Mode.CarrotAttack && timeBefore >= carrotPeriod)
		{
			StartCoroutine(throwCarrot());
			timeBefore = 0;
		}
		else timeBefore += Time.deltaTime;
	}

	private IEnumerator throwCarrot()
	{

		Animator animator = GetComponent<Animator>();


		animator.SetBool("Attack", true);
		launchCarrot(getDirection());
		yield return new WaitForSeconds(0.8f);

		animator.SetBool("Attack", false);
	}

	public GameObject prefabCarrot;
	void launchCarrot(float direction)
	{
		if (direction != 0)
		{
			//Створюємо копію Prefab
			GameObject obj = GameObject.Instantiate(this.prefabCarrot);
			//Розміщуємо в просторі
			obj.transform.position = this.transform.position;
			obj.transform.position += new Vector3(0.0f, 1.0f, 0.0f);
			//Запускаємо в рух
			Carrot carrot = obj.GetComponent<Carrot>();
			carrot.launch(direction);
		}
	}

	private void setMode()
	{
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.Die) return;
		else if (Mathf.Abs(rabbit_pos.x - my_pos.x) < seeOn)
		{
			currentMode = Mode.CarrotAttack;
		}
		else if (currentMode == Mode.GoToA)
		{
			if (isArrived(my_pos, pointA))
			{
				currentMode = Mode.GoToB;
			}
		}
		else if (currentMode == Mode.GoToB)
		{
			if (isArrived(my_pos, pointB))
			{
				currentMode = Mode.GoToA;
			}
		}
		else currentMode = Mode.GoToB;
	}


	private IEnumerator attack(HeroRabbit rabbit)
	{
		Animator animator = GetComponent<Animator>();

		animator.SetBool("Attack", true);
		rabbit.removeOneHealth();
		yield return new WaitForSeconds(0.8f);

		animator.SetBool("Attack", false);

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (currentMode != Mode.Die)
		{
			HeroRabbit rabbit = collision.gameObject.GetComponent<HeroRabbit>();
			if (rabbit != null)
			{
				Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
				Vector3 my_pos = this.transform.position;
				currentMode = Mode.StandartAttack;

				if (currentMode == Mode.StandartAttack && Mathf.Abs(rabbit_pos.y - my_pos.y) < 1.0f)
				{
					StartCoroutine(attack(rabbit));
				}
				else if (currentMode == Mode.StandartAttack && Mathf.Abs(rabbit_pos.y - my_pos.y) > 1.0f)
				{
					currentMode = Mode.Die;
				}

			}
		}
	}

	private void run()
	{

		//[-1, 1]
		float value = this.getDirection();
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Animator animator = GetComponent<Animator>();

		if (value < 0)
		{
			sr.flipX = false;

		}
		else if (value > 0)
		{
			sr.flipX = true;
		}
		if (currentMode != Mode.CarrotAttack)
		{
			if (Mathf.Abs(value) > 0)
			{
				Vector2 vel = myBody.velocity;
				vel.x = value * speed;
				myBody.velocity = vel;
			}


			if (Mathf.Abs(value) > 0)
			{
				animator.SetBool("Run", true);
			}
			else
			{
				animator.SetBool("Run", false);
			}
		} else animator.SetBool("Run", false);
	}


	private float getDirection()
	{
		Vector3 rabbit_pos = HeroRabbit.lastRabbit.transform.position;
		Vector3 my_pos = this.transform.position;

		if (currentMode == Mode.StandartAttack || currentMode == Mode.CarrotAttack)
		{
			//Move towards rabbit
			if (my_pos.x - rabbit_pos.x < -1)
			{
				return 1;
			}
			else if (my_pos.x - rabbit_pos.x > 1)
			{
				return -1;
			}
			else return 0;
		}

		else if (currentMode == Mode.GoToA)
		{
			return -1;
		}
		else if (currentMode == Mode.GoToB)
		{
			return 1;
		}
		return 0;
	}

	private IEnumerator die()
	{
		if (currentMode == Mode.Die)
		{
			Animator animator = GetComponent<Animator>();
			animator.SetBool("Die", true);
			this.GetComponent<BoxCollider2D>().isTrigger = true;

			if (myBody != null) Destroy(myBody);

			yield return new WaitForSeconds(0.8f);

			animator.SetBool("Die", false);
			Destroy(this.gameObject);
		}
	}

	private bool isArrived(Vector3 pos, Vector3 target)
	{
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.2f;
	}
}