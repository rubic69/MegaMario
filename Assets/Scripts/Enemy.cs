using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float velocity = 1f;
	Transform sightStart;
	Transform sightEnd;
	Transform weakness;
	bool dead = false;
	
	bool colliding;
	
	Animator anim;
	
	public LayerMask detectWhat;


	private bool facingRight = true;			// Nustatoma i kuria puse ziures
	private Transform groundCheck = null;			// Pozicija nustatanti kur zaidejas liecia zeme
	private float groundedRadius = .2f;			// Per koki atstuma nustato kur zeme
	private Transform ceilingCheck = null;				// Pozicija nustatanti kur zaidejas liecia lubas
	private float ceilingRadius = .1f;

	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator> ();
	}



	void Awake() {
		// uzbindina
		sightStart = transform.Find("sightStart");
		sightEnd = transform.Find("sightEnd");
		weakness = transform.Find("weakness");
	}

	void FixedUpdate() {
		rigidbody2D.velocity = new Vector2 (velocity, rigidbody2D.velocity.y);
		colliding = Physics2D.Linecast (sightStart.position, sightEnd.position, detectWhat);
		
		if (colliding) {
			
			transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
			velocity*= -1;
			
		}
		// Pass all parameters to the character control script.
		//Move();
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (!dead && col.gameObject.tag == "Player") {
			float height= col.contacts[0].point.y - weakness.position.y;
			if(height>0) {
				dead = true;
				Dies();
				col.rigidbody.AddForce(new Vector2(0,300));	
			} else {
				Player player = (Player) col.gameObject.GetComponent(typeof(Player));
				GameMaster.killPlayer(player);
			}
		}
	}

	void Dies() {
		//anim.SetBool ("stomped", true);
		Destroy (this.gameObject, 1f);
		//gameObject.tag = "neutralized";	
	}

	void Move() {
		/*if (facingRight) {
			rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
		} else {
			rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
		}

		/*if(move > 0 && !facingRight) {
			Flip();
		} else if(move < 0 && facingRight) {
			Flip();
		}*/	
	}

	/*
	*	Metodas skirtas apsukti personaza
	*/
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}




	// Update is called once per frame
	void Update () {
	
	}
}
