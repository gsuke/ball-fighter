using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapEditorController : MonoBehaviour {

	public GameObject p, stage, tile, startPosition;
	public Slider zoomSlider, absoluteHeightSlider, relativeHeightSlider;
	public Text absoluteHeightText, relativeHeightText, sizeXText, sizeZText;
	public Dropdown modeDropdown, tileDropdown;
	public GameObject[] modeObjects;

	private int i, j;
	private float h, v;
	private Vector3 vec;
	private GameObject go;
	private MapJson mj;

	public int mapSizeX, mapSizeZ;
	public float speed;
	public Material[] tileMaterials;

	void Start () {
		// コンポーネント読み込み
		mj = GetComponent<MapJson>();

		// タイル配置
		for (i = 0; i < mapSizeX; i++) {
			for (j = 0; j < mapSizeZ; j++) {
				CreateTile (i, j);
			}
		}
	}

	void Update () {
		PlayerMove (); // プレイヤーの移動等
	}

	void PlayerMove () {

		// プレイヤーの移動
		h = Input.GetAxis("Horizontal"); // 横方向入力の取得
		v = Input.GetAxis("Vertical"); // 縦方向の取得
		p.transform.position += new Vector3(h, 0, v) * speed * Time.deltaTime; // 移動

		// 移動制限
		if (p.transform.position.x < 0) {
			vec = p.transform.position;
			vec.x = 0;
			p.transform.position = vec;
		}
		if (p.transform.position.x > mapSizeX) {
			vec = p.transform.position;
			vec.x = mapSizeX;
			p.transform.position = vec;
		}
		if (p.transform.position.z < 0) {
			vec = p.transform.position;
			vec.z = 0;
			p.transform.position = vec;
		}
		if (p.transform.position.z > mapSizeZ) {
			vec = p.transform.position;
			vec.z = mapSizeZ;
			p.transform.position = vec;
		}

		// ズーム
		p.transform.position = Vector3.Lerp(
			p.transform.position,
			new Vector3(
				p.transform.position.x,
				(1 - zoomSlider.value) * 30.0f + zoomSlider.value * 2.0f,
				p.transform.position.z
			), 0.5f
		);
	}

	// モード変更
	public void ChangeMode () {
		
		// 表示するUIの切り替え
		for (i = 0; i < modeObjects.Length; i++) {
			if (i == modeDropdown.value) {
				modeObjects [i].SetActive (true); // 対象のUIを表示
			} else {
				modeObjects [i].SetActive (false); // それ以外は非表示
			}
		}

		// 初期位置の表示の切り替え
		if (modeDropdown.value == 3) {
			startPosition.GetComponent<Animator> ().SetTrigger ("Show");
		} else {
			startPosition.GetComponent<Animator> ().SetTrigger ("Hide");
		}

		// タイル全体の変更
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stage")) {
			MapEditorTile goT = go.GetComponent<MapEditorTile> ();

			// 0番タイル以外のみ処理
			if (goT.tileNumber != 0) {
				Renderer goR = go.GetComponent<Renderer> ();
				// タイル変更モード・初期位置変更モード
				if (modeDropdown.value == 0 || modeDropdown.value == 3) {
					goT.HideText(); // 文字を非表示
					goR.material.color = new Color (1, 1, 1); // 白い(通常)
				}
				// 高さ変更モード
				else if (modeDropdown.value == 1 || modeDropdown.value == 2) {
					goT.ShowText(); // 文字を表示
					goR.material.color = new Color (0.3f, 0.3f, 0.3f); // 黒い
				}
			}
		}

	}

	// 高さ(絶対)変更
	public void ChangeAbsoluteHeight () {
		absoluteHeightText.text = absoluteHeightSlider.value.ToString ("#0.0");
	}

	// 高さ(相対)変更
	public void ChangeRelativeHeight () {
		relativeHeightText.text = relativeHeightSlider.value.ToString ("#0.0");
	}

	// 横サイズを1拡張
	public void AddSizeX () {
		if (mapSizeX < 50) {
			mapSizeX++;
			for (i = 0; i < mapSizeZ; i++) {
				CreateTile (mapSizeX - 1, i);
			}
			sizeXText.text = "x" + mapSizeX;
		}
	}

	// 縦サイズを1拡張
	public void AddSizeZ () {
		if (mapSizeZ < 50) {
			mapSizeZ++;
			for (i = 0; i < mapSizeX; i++) {
				CreateTile (i, mapSizeZ - 1);
			}
			sizeZText.text = "x" + mapSizeZ;
		}
	}

	// タイルを生成する関数
	void CreateTile ( int x, int z ) {
		go = (GameObject)Instantiate (tile);
		go.transform.position = new Vector3 (x, 0, z);
		go.name = x + "," + z;
		go.transform.SetParent (stage.transform);
	}

	// JSONの作成
	public void CreateJson () {
		mj.Reset ();
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stage")) {
			MapEditorTile goT = go.GetComponent<MapEditorTile> ();
			if (goT.tileNumber != 0) {
				mj.AddBlock (go.transform.position.x, go.transform.position.z, goT.height, goT.tileNumber);
				// 初期位置の設定
				if (startPosition.transform.position.x == go.transform.position.x && startPosition.transform.position.z == go.transform.position.z) {
					mj.SetStartPosition (go.transform.position.x, goT.height + 0.4f, go.transform.position.z);
				}
			}
		}
		GUIUtility.systemCopyBuffer = mj.ToJson ();
	}

}