using UnityEngine;
using System.Collections;

public class Attack_Arrow : Attack {

	private Quaternion angle;
	private Vector3 pos, dir;

	public override void Start () {
		base.Start ();

		pos = transform.position; // 位置を取得
		angle = transform.rotation; // 攻撃者の向きを取得
		transform.position = pos; // 位置設定
		dir = angle * new Vector3 (0, 2.3f, 12.5f); // 移動単位の設定
		transform.SetParent(null); // 親解除
	}

	public override void Update () {
		base.Update ();

		transform.position += dir * Time.deltaTime; // 移動
		dir.y += Physics.gravity.y * Time.deltaTime; // 落下速度を加速
	}

}