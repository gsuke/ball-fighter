using UnityEngine;
using System.Collections;

public class Stage: MonoBehaviour {

	private GameObject go;
	private MapJson.Root root;

	[SerializeField] private GameObject groundBlock;
	[SerializeField] private Material[] materials;
	[SerializeField] private TextAsset map;
	[SerializeField] private Transform spawnPosition;

	void Start () {

		root = GetComponent<MapJson> ().FromJson (map.text); // Jsonの読み込み

		// 初期位置の設定
		spawnPosition.position = new Vector3 (
			root.start_position.x,
			root.start_position.y,
			root.start_position.z
		);

		// フィールドの生成
		foreach (MapJson.Block block in root.blocks) {
			go = (GameObject)Instantiate(groundBlock, new Vector3(block.x, 0, block.z), Quaternion.identity);
			go.transform.parent = transform;
			go.name = "Block(" + block.x + "," + block.z + ")";
			GroundBlock gb = go.GetComponent<GroundBlock> ();
			gb.material = materials [block.material];
			gb.h = block.height;
		}

	}
}
