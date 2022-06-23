using UnityEngine;
using System.Collections;

public class MapJson : MonoBehaviour {

	// Jsonデータの定義
	[System.Serializable] public class Root {
		public StartPosition start_position;
		public Block[] blocks;
	}

	[System.Serializable] public class StartPosition {
		public float x, y, z;
	}

	[System.Serializable] public class Block {
		public float x, z, height;
		public int material;
	}

	// 変数・オブジェクトの宣言
	private Root root = new Root ();
	private StartPosition start_position = new StartPosition();
	private int i;

	public void Reset () {
		i = 0;
		root.blocks = new Block[3000];
	}

	public void SetStartPosition ( float x, float y, float z ) {
		start_position.x = x;
		start_position.y = y;
		start_position.z = z;
		root.start_position = start_position;
	}

	public void AddBlock ( float x, float z, float height, int material ) {
		root.blocks [i] = new Block ();
		root.blocks [i].x = x;
		root.blocks [i].z = z;
		root.blocks [i].height = height;
		root.blocks [i].material = material;
		i++;
	}

	public string ToJson () {
		System.Array.Resize(ref root.blocks, i);
		return JsonUtility.ToJson(root);
	}

	public Root FromJson (string json) {
		return JsonUtility.FromJson<Root>(json);
	}

}
