#pragma strict

	private var facingRight : boolean = true;			// Nustatoma i kuria puse ziures
	
	@SerializeField
	private var maxSpeed : float = 2f;
	
	@SerializeField
	private var jumpForce : float = 400f;
	
	@SerializeField
	private var whatIsGround : LayerMask;
	
	@SerializeField
	private var airControl : boolean = false;
	
	private var groundCheck : Transform = null;;				// Pozicija nustatanti kur zaidejas liecia zeme
	private var groundedRadius : float = .2f;			// Per koki atstuma nustato kur zeme
	private var grounded : boolean = false;				// Zaidejas ant zemes ar ne
	private var ceilingCheck : Transform = null;				// Pozicija nustatanti kur zaidejas liecia lubas
	private var ceilingRadius : float = .1f;			// Per koki atstuma nustato kur lubos

	private var anim : Animator;						// Kintamasis nustatyti animacijai
	
	function Awake() {
		// uzbindina
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent(Animator);
	}
	function FixedUpdate() {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		anim.SetFloat("vSpeed", rigidbody2D.velocity.y);
	}
	/*
	*	Metodas skirtas apsukti personaza
	*/
	function Flip() {
		facingRight = !facingRight;
		var theScale : Vector3 = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	function Move(move : float, jump : boolean) {
		if (grounded || airControl) {
			anim.SetFloat("Speed", Mathf.Abs(move));
			rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
			if(move > 0 && !facingRight) {
				Flip();
			} else if(move < 0 && facingRight) {
				Flip();
			}
		}
		if (grounded && jump) {
            // Add a vertical force to the player.
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
		}
	}