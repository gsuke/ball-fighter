using UnityEngine;
using System.Collections;

public class Buchikakkun : Monster {
	
	public float rotateSpeed;

	private int frame;
	private Vector3 vec, dir;

	// コンポーネント
	private CharacterController cc;
	private CharacterControllerGravity ccg;
	private Animator animator;

	// オブジェクト
	private GameObject attack;

	public override void Start () {
		base.Start ();

		// コンポーネントの取得
		cc = GetComponent<CharacterController> ();
		ccg = GetComponent<CharacterControllerGravity> ();
		animator = GetComponent<Animator> ();

		// オブジェクトの取得
		attack = Resources.Load("Prefabs/Attacks/Attack_Buchikakkun") as GameObject;
	}

	public override void Update () {
		base.Update ();

		// ターゲットが存在しないときは、ターゲットを探す
		if (target == null) {
			SearchTarget (10.0f);
		}

		// ターゲットが存在するとき
		else {

			vec = target.transform.position - transform.position; // ターゲットの方向を算出

			// 非攻撃時(着地時)の挙動
			if (action == "" && cc.isGrounded) {

				dir = Vector3.zero; // 移動ベクトルの初期化

				// 何もしない
				if (frame < 45) {
				}
				// ゆっくりとターゲットの方向を向く
				else if (frame < 90) {
						transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (new Vector3 (vec.x, 0, vec.z)), rotateSpeed * Time.deltaTime);
					}
				// 何もしない
				else if (frame < 135) {
					}
				// 移動
				else if (frame < 180) {
						dir = vec.normalized * speed;
						dir.y = 0;
					}
				// 何もしない
				else if (frame < 225) {
					}
				// ジャンプ
				else if (frame == 225) {
						dir = vec.normalized * speed;
						dir.y = 0;
						ccg.Jump (3.5f);
					}
				// 接地したらフレームを戻す
				else {
					frame = 0;
				}

				// ターゲットと距離が近いときに攻撃モードに移行させる
				if (vec.magnitude <= 1.5f) {
					dir = Vector3.zero;
					action = "Attack";
					frame = 0;
				}

				// ターゲット解除
				if (RemoveTarget(12.0f)) {
					frame = 0;
					dir = Vector3.zero;
					action = "";
				}

			}

			// 攻撃時の挙動
			else if (action == "Attack") {

				// 何もしない
				if (frame < 60) {
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (new Vector3 (vec.x, 0, vec.z)), rotateSpeed * Time.deltaTime);
				}
				// 攻撃準備
				else if (frame == 60) {
					animator.SetTrigger ("AttackReady");
				}
				// 何もしない
				else if (frame < 180) {
				}
				// 攻撃
				else if (frame == 180) {
					animator.SetTrigger ("Attack");
					CreateAttack (attack, 1.0f); // 攻撃の生成
				}
				// 何もしない
				else if (frame < 210) {
				}
				// 攻撃が終了したので通常モードに移行する。
				else if (frame == 210) {
					animator.SetTrigger ("Normal");
				}
				// 何もしない
				else if (frame < 330) {
				}
				// フレームを戻し、通常モードに移行する。
				else {
					frame = 0;
					action = "";
				}

			}

		}

		cc.Move(dir * Time.deltaTime); // 移動処理
		frame++; // フレームカウント
	}
}
