using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

	private float speed;
	private Vector3 dir;
	private Color color;

	void Start () {
		speed = 10.0f;
		dir = Quaternion.Euler (0, 0, Random.Range(-15,16)) * new Vector3 (0, 1, 0);
	}

	void Update () {
		transform.position += dir * speed;
		speed *= 0.9f;
		color = GetComponent<Text> ().color;
		color.a *= 0.9f;
		GetComponent<Text> ().color = color;
	}
}
