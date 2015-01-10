using UnityEngine;
using System.Collections;

public class shooter : MonoBehaviour {

	public GameObject projectile;
	public float speedFactor;
	public float delay;
	Transform weakness;


	void Awake() {
		weakness = transform.Find("weakness");
	}

	// Use this for initialization
	void Start () {
		//StartCoroutine (Shoots ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player") {
			float height= col.contacts[0].point.y - weakness.position.y;
			if(height<0) {
				GameObject clone = (GameObject)Instantiate(projectile, transform.position,Quaternion.identity);
				//clone.rigidbody2D.velocity= -transform.right * speedFactor;
				//clone.rigidbody2D.AddForce(new Vector2(20f,20f));
			}
		}
	}


	IEnumerator Shoots()
	{
		while (true) {


			yield return new WaitForSeconds(delay);

			GameObject clone = (GameObject)Instantiate(projectile, transform.position,Quaternion.identity);

			clone.rigidbody2D.velocity= -transform.right * speedFactor;

				}


	}

}
