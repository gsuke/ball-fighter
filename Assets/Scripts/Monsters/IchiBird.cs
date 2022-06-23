using UnityEngine;
using System.Collections;

public class IchiBird : Monster {

	public float rotateSpeed;

	private int frame;
	private Vector3 vec, dir;
	private Vector2 v2_1, v2_2;

	// コンポーネント
	private CharacterController cc;
	private Animator animator;

	// オブジェクト
	private GameObject attack;
	private ParticleSystem optionCubeParticle;

	public override void Start () {
		base.Start ();

		// コンポーネントの取得
		cc = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> ();

		// オブジェクトの取得
		attack = Resources.Load("Prefabs/Attacks/Attack_IchiBird") as GameObject;
		optionCubeParticle = transform.FindChild ("OptionCubeParticle").GetComponent<ParticleSystem> ();
	}

	public override void Update () {
		base.Update ();

		// ターゲットが存在しないときは、ターゲットを探す
		if (target == null) {
			SearchTarget (10.0f);
		}

		// ターゲットが存在するとき
		else {

			// 非攻撃時(着地時)の挙動
			if (action == "") {

				vec = target.transform.position + new Vector3 (0, 0.3f, 0) - transform.position; // ターゲットの方向を算出
				dir = Vector3.zero; // 移動ベクトルの初期化

				// ゆっくりとターゲットの方向を向く
				if (frame < 90) {
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (vec), rotateSpeed * Time.deltaTime);
					dir = transform.forward * speed;
				}
				// 何もしない
				else if (frame < 150) {
				}
				// ターゲットと距離が近い場合に攻撃モードに移行させ、そうでない場合はフレームを戻す
				else {
					if (vec.magnitude <= 4.0f) {
						dir = Vector3.zero;
						action = "Attack";
					}
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

				vec = target.transform.position - (transform.position + new Vector3 (0, 0.6f, 0)); // ターゲットの方向を算出
				
				// ゆっくりとターゲットの方向を向く
				if (frame < 60) {
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (vec), rotateSpeed * Time.deltaTime);
				}
				// 攻撃準備
				else if (frame == 60) {
					animator.SetTrigger ("AttackReady");
					optionCubeParticle.Play ();
				}
				// 何もしない
				else if (frame < 180) {
				}
				// 攻撃
				else if (frame == 180) {
					CreateAttack (attack, 1.0f); // 攻撃の生成
				}
				// 何もしない
				else if (frame < 240) {
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
