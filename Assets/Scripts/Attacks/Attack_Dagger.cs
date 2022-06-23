using UnityEngine;
using System.Collections;

public class Attack_Dagger : Attack {

	private Quaternion angle;
	private Vector3 pos, dir;

	public override void Start () {
		base.Start ();

		angle = transform.rotation; // 攻撃者の向きを取得
		attackCount = GetComponent<Attack>().attackCount; // 攻撃者の攻撃回数を取得
		transform.position += transform.up * 0.6f + transform.forward * 0.3f; // 位置設定

		// attackCountの値を-10 or 10に変更
		attackCount %= 2;
		if ( attackCount == 0 ) attackCount = -1;
		attackCount *= 10;

		angle *= Quaternion.Euler(0, attackCount, 0); // 角度調整
		dir = angle * new Vector3 (0, -1.0f, 0.6f); // 移動単位の設定
    }

	public override void Update () {
		base.Update ();

		transform.position += dir * 0.12f; // 移動
    }

}