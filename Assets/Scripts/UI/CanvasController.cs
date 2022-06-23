using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    
	// オブジェクト
	public GameController gc;
	private Transform canvas;
	private Slider hpBar, actionBar;
	private SliderValueLerp hpBarLerp;
	private Text hpBarText;
	private Player player;

	void Start () {
		
		// オブジェクトの取得
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		canvas = GameObject.Find ("Canvas").transform;
		hpBar = canvas.FindChild ("HpBar").GetComponent<Slider> ();
		actionBar = canvas.FindChild ("ActionBar").GetComponent<Slider> ();
		hpBarLerp = canvas.FindChild ("HpBar").GetComponent<SliderValueLerp> ();
		hpBarText = hpBar.transform.FindChild("Text").GetComponent<Text>();

	}

	void Update () {

		// プレイヤーオブジェクトがある場合の処理
		if (player) {
			
			hpBarLerp.ChangeValue ((float)player.hp / player.maxHp); // HPバーの変更
			hpBarText.text = (Mathf.RoundToInt (player.maxHp * hpBar.value)) + " / " + player.maxHp; // HPバーのValueから表示する数値を計算

			// アクションバーの変更
			if (player.action == "") {
				actionBar.value = 1;
			} else {
				actionBar.value = (float)player.frame / player.maxFrame;
			}
		
		}

		// プレイヤーオブジェクトがない場合の処理
		else {
			// プレイヤオブジェクトの取得
			if (gc.player) {
				player = gc.player.GetComponent<Player> ();
			}
		}

	}
}
