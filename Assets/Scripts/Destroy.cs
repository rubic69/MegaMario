using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	public bool byTime;
	public bool byContact;
	public float destroyTime;

	// Use this for initialization
	void Start () {
		rigidbody2D.AddForce(new Vector2(50f, 200f));
		Destroy (this.gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player") {
			Destroy (this.gameObject);
			Player player = (Player) col.gameObject.GetComponent(typeof(Player));
			player.increaseCoins();
		}
	}
}
