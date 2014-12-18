using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
		if (gm == null) {
			Debug.Log ("gm");
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}

	public Transform playerPrefab;
	public Transform spawnPointPrefab;
	public int spawnDelay = 2;

	public IEnumerator respawnPlayer2() {
		Debug.Log ("respawnPlayer");
		yield return new WaitForSeconds (spawnDelay);

		Debug.Log ("respawned");
		Instantiate (playerPrefab, spawnPointPrefab.position, spawnPointPrefab.rotation);
	}

	public void respawnPlayer() {
		
		Debug.Log ("respawned");
		Instantiate (playerPrefab, spawnPointPrefab.position, spawnPointPrefab.rotation);
	}


	public static void killPlayer(Player player) {
		Destroy (player.gameObject);
		Debug.Log("DEAD 1 " + gm);
		gm.StartCoroutine (gm.respawnPlayer2());
	}
}
