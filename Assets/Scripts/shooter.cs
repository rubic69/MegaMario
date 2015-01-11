using UnityEngine;
using System.Collections;

public class shooter : MonoBehaviour {

	private Player character;
	
	public GameObject projectile;
	public float speedFactor;
	public float delay;
	Transform weakness;
	

	//public breakSound;


	void Awake() {
		weakness = transform.Find("weakness");
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player") {
			audio.Play();
			float height= col.contacts[0].point.y - weakness.position.y;
			if(height<0) {
				GameObject clone = (GameObject)Instantiate(projectile, transform.position,Quaternion.identity);
			//	PlaySound(0);
				Destroy(this.gameObject);
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
