using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	private Vector3 pos, vec;
	private Quaternion angle, targetAngle, vAxis;
	private float vRotation;
	private int frame;

	// オブジェクト
	public GameObject target;
	private GameController gc;
	
    void Start () {
		frame = -1;

		// オブジェクトの取得
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
	}

	void LateUpdate () {

		// ターゲットがあるときの指定
		if (target) {

			// ダメージを受けていたなら、回転させる。
			if (frame != -1) {
				frame++;
				if (frame < 40) {
					vRotation -= 15;
				} else if (frame == 40) {
					vRotation = 10;
				} else if (frame >= 170) {
					frame = -1;
				}
			} else {
				vRotation = 5;
			}

			// 通常時の処理
			targetAngle = target.transform.rotation; // ターゲットの向きを取得
			pos = target.transform.position + targetAngle * new Vector3 (0, 0.2f, -1.0f); // 移動先の座標を計算

		}

		// ターゲットがいないときは上のほうへ
		else {
			targetAngle = Quaternion.Euler (90, 0, 0);
			pos = new Vector3 (10, 30, 10);
			vRotation = 0;
			// GameControllerからプレイヤーオブジェクトを取得
			if (gc.player) {
				target = gc.player;
			}
		}

		// 移動先の角度を計算
		vAxis = Quaternion.AngleAxis(vRotation, Quaternion.Euler(0, targetAngle.eulerAngles.y, 0) * Vector3.right); // 上下の回転を作成
		angle = vAxis * targetAngle; // 作成した上下の回転をターゲットの向きと合成する
        
		transform.position = Vector3.Lerp (transform.position, pos, 0.1f); // 線形補間
		transform.rotation = Quaternion.Slerp (transform.rotation, angle, 0.1f); // 球面補間

	}

	public void Damage () {
		if (frame == -1) {
			frame = 0;
		}
	}
}
