using UnityEngine;
using System.Collections;

public class PlayablePlayer : MonoBehaviour {

	public float cameraSpeed = 720, h, v, speedMgn = 1;

	// コンポーネント
	private Player p;
	private CharacterController cc;

	// オブジェクト
	public GameObject sphere;

	void Start () {
		// コンポーネントの取得
		p = GetComponent<Player>();
		cc = GetComponent<CharacterController>();

		// オブジェクトの取得
		sphere = transform.FindChild("Sphere").gameObject;
	}

	void Update () {

		// 方向入力の取得(通常時、ダメージ時後半)
		if ( p.action == "" || (p.action == "Damage" && p.frame > p.maxFrame / 3) ) {
			h = Input.GetAxis("Horizontal"); // 横方向
			v = Input.GetAxis("Vertical"); // 縦方向
		}

		// 入力の取得
		if ( p.action == "" ) {
			// 回避入力の取得
			if (Input.GetButtonDown ("Avoid")) {
				// 方向転換
				transform.rotation *= Quaternion.Euler (
					0,
					Mathf.Atan2 (h, v) * Mathf.Rad2Deg,
					0
				);
				// 入力方向を前方にし、速度UP
				speedMgn = 4;
				v = 1;
				p.Avoid ();
			}
			// 攻撃入力の取得
			else if (Input.GetButtonDown("Attack")) {
				p.Attack ();
			}
			// 武器変更入力の取得
			else if (Input.GetButtonDown("Weapon1")){
				p.ChangeWeapon (0);
			}
			else if (Input.GetButtonDown("Weapon2")){
				p.ChangeWeapon (1);
			}
			else if (Input.GetButtonDown("Weapon3")){
				p.ChangeWeapon (2);
			}
		}

		// 回避中の処理
		if (p.action == "Avoid") {
			decelerateAngle ();
			// 減速
			if (p.frame <= 20) {
				speedMgn *= 0.95f;
			}
			// 停止
			if (p.frame == 21) {
				speedMgn = 1;
				v = 0;
			}
		}

		// 攻撃中の処理
		else if (p.action == "Attack") {
			// 動作の減速
			decelerateMoving ();
			decelerateAngle ();
		}

		// ダメージ中の処理
		else if (p.action == "ChangeWeapon") {
			// 動作の減速
			decelerateMoving ();
			decelerateAngle ();
		}

		// ダメージ中の処理
		else if (p.action == "Damage") {
			// 動作の減速
			decelerateMoving();
			decelerateAngle();
			if (p.frame < p.maxFrame / 3) {
				// 何もしない
			} else if (p.frame == p.maxFrame / 3) {
				speedMgn = 3;
			} else if (p.frame+1 < p.maxFrame) {
				// 何もしない
			} else {
				speedMgn = 1;
			}
		}

		cc.Move( (transform.rotation * Vector3.forward) * v * p.speed * speedMgn * Time.deltaTime); // 移動
		transform.Rotate (0, h * cameraSpeed * Time.deltaTime, 0); // 横回転
		sphere.transform.Rotate (v * p.speed * speedMgn, 0, 0); // 縦回転

	}

	// 移動速度を落としていく関数
	void decelerateMoving () {
		if (Mathf.Abs (v) < 0.1) {
			v = 0;
		} else {
			v *= 0.9f;
		}
	}

	// 方向回転速度を落としていく関数
	void decelerateAngle () {
		if (Mathf.Abs (h) < 0.1) {
			h = 0;
		} else {
			h *= 0.9f;
		}
	}

}
