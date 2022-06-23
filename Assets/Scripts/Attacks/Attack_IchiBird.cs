using UnityEngine;
using System.Collections;

public class Attack_IchiBird : MonoBehaviour {

	public float speed;

	void Start () {
		transform.position += transform.up * 0.6f;
		transform.SetParent (null);
	}

	void Update () {
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
