using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject player;

	public GameObject[] weaponPrefab;
	public int[] weaponFrame;
	public float[] weaponMgn;

	public GameObject[] monsters;
	public int defaultSpawn;
	public int spawnInterval;

	void Update () {
		int monsters = 0;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Character")) {
			Character goC = go.GetComponent<Character> ();
			if (goC == null) {
				continue;
			}
			if (goC.role == "Enemy") {
				monsters++;
			}
		}
		if ( monsters <= 3 ) {
			for ( int i = 0; i < 12; i++ ) {
				CreateMonster ();
			}
		}
	}

	void CreateMonster () {
		Instantiate (monsters [Random.Range (0, monsters.Length)], new Vector3 (
			Random.Range (3.0f, 17.0f),
			11.0f,
			Random.Range (3.0f, 17.0f)
		), Quaternion.identity);
	}
}
