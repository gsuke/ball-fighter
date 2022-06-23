using UnityEngine;
using System.Collections;

public class KillMyself : MonoBehaviour {

	// 一定フレーム後に自殺するスクリプト

	public int frame;
	private int i;

	void Start () {
		i = 0;
	}

	void Update () {
		if (i >= frame) {
			Destroy (this.gameObject);
		} else {
			i++;
		}
	}
}
