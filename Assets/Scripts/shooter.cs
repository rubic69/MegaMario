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

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player") {
			float height= col.contacts[0].point.y - weakness.position.y;
			if(height<0) {
				GameObject clone = (GameObject)Instantiate(projectile, transform.position,Quaternion.identity);
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
