using UnityEngine;
using System.Collections;

public class AvoidEffect : MonoBehaviour {

	private Light lightComponent;
	private int frame;
	private GameObject go;
	private int i;

	public GameObject avoidTrail;

	void Start () {
		lightComponent = gameObject.GetComponent<Light> (); // Lightコンポーネントを取得
		frame = -1; // frameが-1の場合はUpdateの内容が動作しない
		// avoidTrailを複製する。
		for (i = 0; i < 20; i++) {
			go = Instantiate (avoidTrail);
			go.transform.SetParent (transform);
		}
	}

	void Update () {
		if (frame != -1) {
			frame++;
			if (frame <= 10) {
				lightComponent.intensity += 0.3f;
			} else if (frame >= 17 && frame < 27) {
				lightComponent.intensity -= 0.3f;
			} else if (frame >= 27) {
				lightComponent.enabled = false;
				lightComponent.intensity = 0;
				frame = -1;
				foreach (Transform child in transform) {
					child.GetComponent<TrailRenderer> ().enabled = false;
				}
			}
		}
	}

	public void EffectStart () {
		lightComponent.enabled = true;
		frame = 0;
		foreach (Transform child in transform) {
			child.GetComponent<TrailRenderer> ().enabled = true; // トレイルレンダラーを有効にする
			child.GetComponent<TrailRenderer> ().Clear(); // トレイルを消す
			// 位置をランダムに配置
			child.position = transform.position + Quaternion.Euler (
				Random.Range (0, 361),
				Random.Range (0, 361),
				Random.Range (0, 361)
			) * new Vector3 (Random.Range (0.0f, 0.4f), 0, 0);
		}

	}
}
