using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HeroRabbit : MonoBehaviour {
	public UILabel CoinsLabel;
	public UILabel FruitsLabel;

	public float speed = 1;
	public float jumpTime = 0f;
	public float maxJumpTime = 2f;
	public float jumpSpeed = 2f;
	float timeToWait = 0f;
	public int health = 2;
	int count;
	int count1;
	bool isGrounded = false;
	bool jumpActive = false;
	bool dead = false;


	Transform heroParent = null;
	Rigidbody2D myBody = null;
	public static HeroRabbit lastRabbit = null;
	void Awake()
	{
		lastRabbit = this;
	}
	// Use this for initialization
	void Start () {
		count = 0;
		count1 = 0;
		SetCountText ();
		SetCountText1 ();
		myBody = this.GetComponent<Rigidbody2D> ();
		LevelControll.current.setStartPosition (transform.position);
		this.heroParent = this.transform.parent;
	}

	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate (){
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		Animator animator = GetComponent<Animator> ();

		float value = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (value) > 0) {
			Vector2 vel = myBody.velocity;
			vel.x = value * speed;
			myBody.velocity = vel;
		}


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

			if (health > 0) {
				if (timeToWait <= 0) {
					dead = false;
					animator.SetBool ("dead", false);
					LevelControll.current.onRabbitDeath (this);
					//StartCoroutine ("dead");

				} else { 
					return;
				}
			} else if (health == 0){
				if (timeToWait <= 0) {
					animator.SetBool ("dead", true);

					this.GetComponent<BoxCollider2D> ().isTrigger = true;

					//yield return new WaitForSeconds(3.0f);

					animator.SetBool("dead", false);
					this.GetComponent<BoxCollider2D> ().isTrigger = false;

					SceneManager.LoadScene("LevelChooseScene");

					//////////////////////////needs changing!
					/// 
				}
			}

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
		if (health > 2 ){
			health = 2;
		}
		if(health < 2 && health > 0){
			health++;
		}
	}

	public void removeOneHealth(){

		if (health == 0){
			dead = true;
		}else if (health > 0){
			health--;

			LevelControll.current.onRabbitDeath(this);

		}

	}
	void OnTriggerEnter2D(Collider2D collider) {

		if (collider.gameObject.CompareTag ("Coin")){
			collider.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
		if (collider.gameObject.CompareTag ("Fruit")){
			collider.gameObject.SetActive (false);
			count = count1 + 1;
			SetCountText1 ();
		}
	}
	public void SetCountText (){
		CoinsLabel.text = count.ToString ();

	}
	public void SetCountText1 (){
		FruitsLabel.text = count.ToString ();

	}

}
