#pragma strict

	@script RequireComponent(Character_megaman);

	private var character : Character_megaman;
	private var jump : boolean;
	private var slide : boolean;

	function Awake() {
		character = GetComponent("Character_megaman");
	}
	
	function Update() {
		if(Input.GetButton("Jump")) {
			jump = true;
		}
		if(Input.GetKey(KeyCode.B)) {
			slide = true;
		}
	}
	
	function FixedUpdate() {
		// Read the inputs.
		var h : float = Input.GetAxis("Horizontal");
		// Pass all parameters to the character control script.
		character.Move( h, jump , slide);
        // Reset the jump input once it has been used.
	    jump = false;
	    slide = false;
	}