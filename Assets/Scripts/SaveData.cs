using UnityEngine;
using System.Collections;

public class SaveData : MonoBehaviour {

	public static Root saveData; // シーン間で保持されるセーブデータ
	public static string userId, password; // シーン間で保持されるID、パスワード情報

	// Jsonデータの定義
	[System.Serializable] public class Root {
		public string name;
		public string email;
		public int sex;
		public string moodMessage;
		public string created;
		public string logined;
		public int hp;
		public int atk;
		public int level;
		public int skin;
		public int money;
		public int weapon1;
		public int weapon2;
		public int weapon3;
	}

	// セーブデータをセットする関数
	public void SetSaveData ( string json ) {
		saveData = JsonUtility.FromJson<Root>(json);
	}
}
