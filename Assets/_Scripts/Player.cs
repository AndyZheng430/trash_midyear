using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Animator anim;

	public int health = 10;
	public int count = 0;
	public GameObject[] ammo;

	public Text HP;
	public Text score;

	public Rigidbody2D rb;
	public bool faceRight = true;
	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		HP.text = "HP: " + health; //health update
		score.text = "SCORE: " + count; // score update
 		rb = this.gameObject.GetComponent<Rigidbody2D> ();
	}
	//turning
	void Flip(){
		faceRight = !faceRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
	// Update is called once per frame
	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");

		rb.velocity = new Vector2 (move * 10f, rb.velocity.y);
		anim.SetFloat ("vspeed", rb.velocity.y);
		anim.SetFloat ("speed", Mathf.Abs (move)); // turns if velocity is negative  

		if (move > 0 && !faceRight) {
			Flip ();
		} else if (move < 0 && faceRight) {
			Flip ();
		}
		if (this.gameObject.transform.position.y <= -10) { // if character falls below -10 dies
			anim.Play ("death");
			Destroy (this.gameObject,1);
		}
	}
	void Update(){
		//jump
		if (Input.GetButtonDown("Vertical")) {
			rb.AddForce (new Vector2 (0, 400f));
		}
	}
	IEnumerator fire(){ // shooting
		yield return new WaitForSeconds (1f);
	}
	void OnTriggerEnter2D(Collider2D other){

	}
	void setHP(){ //health decreases
		HP.text = "HP: " + health;
	}
	void setScore(){ //score increases
		score.text = "Score: " + count;
	}
}
