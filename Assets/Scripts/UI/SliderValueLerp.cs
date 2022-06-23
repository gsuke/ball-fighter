using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderValueLerp : MonoBehaviour {

	// このスクリプトは Slider に追加すること。

	private float newValue;
	private Slider slider;

	void Start () {
		slider = GetComponent<Slider> ();
	}

	void Update () {
		slider.value = 0.9f * slider.value + 0.1f * newValue;
		// 誤差補正
		if (Mathf.Abs(newValue - slider.value) <= 0.001f) {
			slider.value = newValue;
		}
	}

	public void ChangeValue( float n ) {
		newValue = n;
	}
}
