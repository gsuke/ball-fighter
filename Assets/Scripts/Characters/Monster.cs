using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Monster : Character {
    
	private GameObject dieEffect;

	public override void Start () {
		base.Start ();
		dieEffect = Resources.Load ("Prefabs/Effects/DieEffect") as GameObject;
	}

	public override void Update () {
		base.Update ();
	}

	// ダメージを受ける処理
	public override void Damage (int dmg) {
		base.Damage (dmg);

		if (hp <= 0) {
			Instantiate (dieEffect, transform.position, Quaternion.Euler (270, 0, 0)); // 死亡時にパーティクル生成
			CharacterDestroy(); // キャラクター破棄
		}
	}
}
