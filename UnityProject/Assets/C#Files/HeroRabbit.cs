using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
	public float speed = 1;
	public float jumpTime = 0f;
	public float maxJumpTime = 2f;
	public float jumpSpeed = 2f;
	int currentHealth = 1;
	float timeToWait = 0.0f;
	float redTime = 4.0f;
	public int health = 1;

	bool isGrounded = false;
	bool jumpActive = false;
	bool dead = false;


	public bool rabbitRed = false;
	public bool rabbitBig = false;
	public bool makeRabbitBig = false;

	Transform heroParent = null;
	Rigidbody2D myBody = null;
	public static HeroRabbit lastRabbit = null;
	void Awake()
	{
		lastRabbit = this;
	}
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

		if (this.dead){
			animator.SetBool("dead", true);
			timeToWait -= Time.deltaTime;

			if (currentHealth > 0) {
				if (timeToWait <= 0) {
					dead = false;
					animator.SetBool ("dead", false);
					LevelControll.current.onRabbitDeath (this);
					//StartCoroutine (dead ());

				} else { 
					return;
				}
			} else {
				if (timeToWait <= 0) {
					dead = false;
					animator.SetBool ("dead", false);
					// just to delete a rabbit for a first time!!!!!!
					gameObject.GetComponent<Renderer>().enabled = false;
					speed = 0;

					//////////////////////////needs changing!
					/// 
				}
			}
		}


		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		if (rabbitRed) {
			redTime -= Time.deltaTime;
			sr.color = Color.red;
			if (redTime <= 0)
				rabbitRed = false;
		} else {
			sr.color = Color.white;
		}
		if (!rabbitBig && makeRabbitBig) {
			this.transform.localScale = new Vector3 (2, 2, 0);
			rabbitBig = true;
		} else if (rabbitBig && !makeRabbitBig) {
			this.transform.localScale = new Vector3 (1, 1, 0);
			rabbitBig = false;
		} 
			
		if(value < 0) {
			sr.flipX = true;
		} else if(value > 0) {
			sr.flipX = false;
		}
		//class HeroRabbit, void FixedUpdate()
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
			this.jumpActive = true;
		}
		if (this.jumpActive){
			//Якщо кнопку ще тримають
			if (Input.GetButton("Jump")){
				this.jumpTime += Time.deltaTime;
				if (this.jumpTime < this.maxJumpTime)
				{
					Vector2 vel = myBody.velocity;
					vel.y = jumpSpeed * (1.0f - jumpTime / maxJumpTime);
					myBody.velocity = vel;
				}
			}else{
				this.jumpActive = false;
				this.jumpTime = 0;
			}
		}
		if (this.isGrounded){
			animator.SetBool("jump", false);
		}
		else{
			animator.SetBool("jump", true);
		}
	}
	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = obj.transform.position;
			//Встановлюємо нового батька
			obj.transform.parent = new_parent;
			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			obj.transform.position = pos;
		}
	}
	public void addOneHealth(){
		if (currentHealth < health){
			currentHealth++;
		}
		if(currentHealth == health){
			makeRabbitBig = true;
		}
	}

	public void removeOneHealth(){
		if (!rabbitRed){
			if (currentHealth > 1){
				currentHealth--;
				makeRabbitBig = false;
				rabbitRed = true;
				redTime = 4.0f;
			}else{
				currentHealth--;
				timeToWait = 1.0f;
				dead = true;
			}
		}
	}
}
