using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player") {
			float height= col.contacts[0].point.y - transform.position.y;
			Application.LoadLevel(2);
		}
	}
}
