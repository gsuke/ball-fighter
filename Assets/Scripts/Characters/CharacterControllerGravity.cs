using UnityEngine;
using System.Collections;

public class CharacterControllerGravity : MonoBehaviour {

	private CharacterController cc;
	private Vector3 dir;

	void Start () {
		cc = GetComponent<CharacterController> ();
		dir = Vector3.zero;
	}

	void Update () {
		
		dir.y += Physics.gravity.y * Time.fixedDeltaTime; // 落下速度を加速

		cc.Move(dir * Time.deltaTime); // 移動

		// 接地時には落下速度を0にする
		if ( cc.isGrounded ) {
			dir.y = 0;
		}

	}

	// ジャンプ処理
	public void Jump (float power) {
		dir.y = power;
		cc.Move(dir * Time.deltaTime); // isGrounded判定されないようにするため、移動する
	}

	// 落下速度リセット
	public void ResetGravity () {
		dir = Vector3.zero;
	}
}
