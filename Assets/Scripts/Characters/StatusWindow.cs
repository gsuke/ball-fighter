using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusWindow : MonoBehaviour {

	public GameObject target;

	private GameObject canvas;
	private Character targetC;
	private Animator animator;
	private Text swName, swHpText;
	private Slider swHp;
	private SliderValueLerp swHpLerp;

	void Start () {
		// 取得
		canvas = GameObject.Find ("Canvas");
		animator = GetComponent<Animator>();
		swName = transform.FindChild ("Name").GetComponent<Text> (); // 名前欄の取得
		swHp = transform.FindChild ("HpBar").GetComponent<Slider> (); // HPバーの取得
		swHpLerp = swHp.GetComponent<SliderValueLerp>(); // HPバーのSliderLerpを取得
		swHpText = transform.FindChild ("HpText").GetComponent<Text> (); // HPテキストの取得
		targetC = target.GetComponent<Character>();

		// 設定
		transform.SetParent(canvas.transform);
		swName.text = targetC.charactorName; // 名前欄に名前を反映
	}

	void Update () {
		// targetが存在しなければこのステータスウィンドウを削除
		if (!target) {
			Destroy (this.gameObject);
			return;
		}

		// ステータスウィンドウの座標を移動
		transform.position = Camera.main.WorldToScreenPoint(target.transform.position) + new Vector3(0, 65, 0); 

		// カメラの位置が近く、カメラがこちらを向いていればステータスウィンドウを表示にする
		if ( (target.transform.position - Camera.main.transform.position ).magnitude < 5.0f 
			&& Vector3.Angle( Camera.main.transform.forward, Camera.main.transform.position - target.transform.position ) > 90 ) {
			if ( !animator.GetBool("Enabled")) {
				animator.SetTrigger("Show");
				animator.SetBool("Enabled", true);
			}
		// カメラの位置が遠かったり、カメラがこちらを向いていない場合はステータスウィンドウを非表示にする
		} else if ( animator.GetBool("Enabled") ) {
			animator.SetTrigger("Hide");
			animator.SetBool("Enabled", false);
		}

		// HPバーの変更
		swHpLerp.ChangeValue ((float)targetC.hp / targetC.maxHp); // Valueの変更
		swHpText.text = (Mathf.RoundToInt (targetC.maxHp * swHp.value)) + " / " + targetC.maxHp; // HPテキストの変更
	}
}
