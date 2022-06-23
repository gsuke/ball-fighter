using UnityEngine;
using System.Collections;

public class MapEditorTile : MonoBehaviour {

	[SerializeField] private GameObject text;

	private MapEditorController mec;
	public int tileNumber;
	public float height;

	void Start () {
		tileNumber = 0;
		SetHeight (10);
		mec = GameObject.FindGameObjectWithTag ("GameController").GetComponent<MapEditorController> ();
	}

	// オブジェクト上でクリックしたときと、クリックしたままオブジェクトに入ったときにイベントを実行
	void OnMouseDown () {
		ClickEvent ();
	}
	void OnMouseEnter () {
		if (Input.GetMouseButton(0)) {
			ClickEvent ();
		}
	}

	void ClickEvent () {
		// タイル変更モード
		if (mec.modeDropdown.value == 0) {
			ChangeTileNumber (mec.tileDropdown.value); // タイル番号
		}
		// 高さ変更(絶対)モード
		else if (mec.modeDropdown.value == 1) {
			SetHeight (mec.absoluteHeightSlider.value);
		}
		// 高さ変更(相対)モード
		else if (mec.modeDropdown.value == 2) {
			AddHeight (mec.relativeHeightSlider.value);
		}
		// 初期位置変更モード
		else if (mec.modeDropdown.value == 3) {
			mec.startPosition.transform.position = new Vector3 (transform.position.x, 0.4f, transform.position.z);
		}
	}

	public void ChangeTileNumber (int n) {
		tileNumber = n;
		GetComponent<Renderer> ().material = mec.tileMaterials [n]; // マテリアルの変更
		// 0番に変更された場合はheightも初期化
		if (n == 0) {
			height = 10;
		}
	}

	public void HideText () {
		text.SetActive (false);
	}

	public void ShowText () {
		text.SetActive (true);
	}

	public void SetHeight (float n) {
		height = n;
		text.GetComponent<TextMesh> ().text = height.ToString ("#0.0");
	}

	public void AddHeight (float n) {
		SetHeight (height + n);
		if (height < 1) {
			SetHeight (1);
		}
		if (height > 20) {
			SetHeight (20);
		}
	}
}
