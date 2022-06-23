using UnityEngine;
using System.Collections;

public class GroundBlock : MonoBehaviour {

	public float h;
	public Vector3 vec;
	public Material material;

	private GameObject up, down, forward, back, left, right;

	void Start () {
		// オブジェクトの取得
		up = transform.FindChild("Up").gameObject;
		down = transform.FindChild("Down").gameObject;
		forward = transform.FindChild("Forward").gameObject;
		back = transform.FindChild("Back").gameObject;
		left = transform.FindChild("Left").gameObject;
		right = transform.FindChild("Right").gameObject;

		// 縦幅を調整
		vec = transform.localScale;
		vec.y = h;
		transform.localScale = vec;

		// Y座標を調整
		vec = transform.position;
		vec.y = h / 2;
		transform.position = vec;

		// マテリアルの変更
		ChangeMaterial(new GameObject[6] {up, down, forward, back, left, right}, material);

		// Tilingの調整
		ChangeTilingY(new GameObject[4] {forward, back, left, right}, h);
	}

	public void ChangeMaterial (GameObject[] gos, Material m) {
		foreach (GameObject go in gos) {
			go.GetComponent<Renderer> ().material = m;
		}
	}

	public void ChangeTilingY (GameObject[] gos, float f) {
		foreach (GameObject go in gos) {
			go.GetComponent<Renderer> ().material.mainTextureScale = new Vector2(3, 3 * f);
		}
	} 
}
