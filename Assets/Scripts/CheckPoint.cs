using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public Transform player;
	

	// Update is called once per frame
	void Update () {
		if (player.transform.position.x >= transform.position.x) {
			Debug.Log("finish");
		}
	}
}
