using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool facingRight = true;			// Nustatoma i kuria puse ziures
	public float maxSpeed = 2f;
	public float jumpForce = 400f;
	public float slideForce = 20;
	public LayerMask whatIsGround;
	public bool airControl = false;
	
	private Transform groundCheck = null;			// Pozicija nustatanti kur zaidejas liecia zeme
	private float groundedRadius = .2f;			// Per koki atstuma nustato kur zeme
	private bool grounded = false;				// Zaidejas ant zemes ar ne
	private Transform ceilingCheck = null;				// Pozicija nustatanti kur zaidejas liecia lubas
	private float ceilingRadius = .1f;			// Per koki atstuma nustato kur lubos
	
	private Animator anim;						// Kintamasis nustatyti animacijai
	
	private float lastSpeed = 0;
	
	public float slideSpeed = 20; // slide speed
	private bool isSliding = false;
	private float slideTimer = 0.0f;
	public float slideTimerMax = 0.5f; // time while sliding

	private bool jump = false;

	void Awake() {
		// uzbindina
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		if (!isSliding) {
			anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
		}
		// Read the inputs.
		float h = Input.GetAxis("Horizontal");
		// Pass all parameters to the character control script.
		Move( h, jump);
		// Reset the jump input once it has been used.
		jump = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Jump")) {
			jump = true;
		}
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

	void Move(float move, bool jump) {
		float currSpeed = Mathf.Abs(move) * maxSpeed;
		
		if (grounded || airControl) {
			//disable run animation when sliding
			if (!isSliding) {
				anim.SetFloat("Speed", Mathf.Abs(move));
			}
			
			if (grounded && !isSliding) {
				if (lastSpeed >= maxSpeed-1) {
					if(move >= 0 && !facingRight) {
						Debug.Log("left");
						isSliding = true;
						slideTimer = 0.0f; 
						rigidbody2D.AddForce(new Vector2(-slideForce, 0f));
					} else if(move <= 0 && facingRight) {
						Debug.Log("right");
						isSliding = true;
						slideTimer = 0.0f; 
						rigidbody2D.AddForce(new Vector2(slideForce, 0));
					}	
				}
			}
			
			slideTimer += Time.deltaTime;
			if (slideTimer > slideTimerMax) {
				isSliding = false;
			}
			
			if(!isSliding) {
				rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
				if(move > 0 && !facingRight) {
					Flip();
				} else if(move < 0 && facingRight) {
					Flip();
				}
			}	
		}
		
		
		if (grounded && jump) {
			// Add a vertical force to the player.
			anim.SetBool("Ground", false);
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
		}
		lastSpeed = currSpeed;
	}
}
