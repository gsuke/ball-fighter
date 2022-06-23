using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Attack : Character {

	public int attackCount;
	public float mgn;

	// オブジェクト
	private GameObject damageEffect, canvas, damageText;

	public override void Start () {
		base.Start ();

		// オブジェクトの取得
		canvas = GameObject.Find ("Canvas");
		damageText = Resources.Load ("Prefabs/Effects/DamageText") as GameObject;
		damageEffect = Resources.Load ("Prefabs/Effects/DamageEffect") as GameObject;
	}

	public override void Update () {
		base.Update ();
	}

	// 敵対キャラクターとの当たり判定
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Character") { // Characterスクリプトを保持しているかどうかを判定
			Character colC = col.GetComponent<Character> (); // Characterスクリプトを取得

			// roleが異なり、無敵状態でないとき攻撃が成立する
			if ( role != colC.role && !colC.invincible ){

				int dmg = Mathf.RoundToInt ( atk * mgn * Random.Range(0.9f, 1.1f) ); // ダメージ量の計算
				colC.Damage (dmg); // ダメージを与える

				// 「操作キャラ以外」かつ「カメラの視界内に入っている」場合にダメージテキストを生成
				if (colC.role != "Me" && Vector3.Angle( Camera.main.transform.forward, Camera.main.transform.position - transform.position ) > 120) {
					GameObject go = (GameObject)Instantiate (damageText, Camera.main.WorldToScreenPoint (transform.position + new Vector3(0, -0.3f, 0)), Quaternion.identity);
					go.transform.SetParent (canvas.transform); // 親をCanvasに設定
					go.GetComponent<Text> ().text = dmg.ToString (); // ダメージテキストにダメージ量を反映
				}

				Instantiate(damageEffect, transform.position, Quaternion.identity); // ダメージエフェクト生成

			}

		}
	}
}
