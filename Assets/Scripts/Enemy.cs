using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float velocity = 1.0f;
	Transform sightStart;
	Transform sightEnd;
	Transform weakness;
	public GameObject projectile;
	bool dead = false;
	
	bool colliding;

	
	public LayerMask detectWhat;

	Animator anim;


	private bool facingRight = true;			// Nustatoma i kuria puse ziures
	private Transform groundCheck = null;			// Pozicija nustatanti kur zaidejas liecia zeme
	private float groundedRadius = .2f;			// Per koki atstuma nustato kur zeme
	private Transform ceilingCheck = null;				// Pozicija nustatanti kur zaidejas liecia lubas
	private float ceilingRadius = .1f;

	// Use this for initialization
	void Start () {
		velocity = Random.Range(-1.0f, 1.0f);
		if (velocity <= 0) {
			velocity = -1.0f;
			transform.localScale = new Vector2 (transform.localScale.x * -1, transform.localScale.y);
		} else {
			velocity = 1.0f;
		}

		//anim = GetComponent<Animator> ();
	}



	void Awake() {
		// uzbindina
		sightStart = transform.Find("sightStart");
		sightEnd = transform.Find("sightEnd");
		weakness = transform.Find("weakness");
		anim = GetComponent<Animator>();
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
				Instantiate(projectile, transform.position,Quaternion.identity);
				Instantiate(projectile, transform.position,Quaternion.identity);
				Dies();
				col.gameObject.rigidbody2D.AddForce(new Vector2(0,600));	
			} else {
				dead = true;
				Player player = (Player) col.gameObject.GetComponent(typeof(Player));
				player.damagePlayer(40);
				col.gameObject.rigidbody2D.AddForce(new Vector2(0,600));	
				Dies();
			}
		}
	}

	void Dies() {
		anim.SetBool ("Dead", true);
		audio.Play ();
		Destroy (this.gameObject, 0.4f);
		//gameObject.tag = "neutralized";	
	}

	// Update is called once per frame
	void Update () {
	
	}
}
