using UnityEngine;
using System.Collections;

public class Attack_Sword : Attack {

	private Quaternion angle;
	private Vector3 pos, axis;

	public override void Start () {
		base.Start ();

		pos = transform.position; // 位置を取得
		angle = transform.rotation; // 向きを取得
		attackCount = GetComponent<Attack>().attackCount; // 攻撃回数を取得
		transform.position += new Vector3 (0, 0.7f, 0); // 位置設定

        // attackCountの値を-10 or 10に変更
        attackCount %= 2;
        if ( attackCount == 0 ) attackCount = -1;
        attackCount *= 10;

        // 回転軸を設定
		axis = Quaternion.Euler(0, attackCount, 0) * angle * new Vector3(1, 0, 0);
    }

	public override void Update () {
		base.Update ();

		transform.RotateAround(pos, axis, 120.0f / 20); // 回転
    }

}
