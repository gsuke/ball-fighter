using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderColorLerp : MonoBehaviour {

	// このスクリプトは Slider > Fill Area > Fill に追加すること。

	public Color minColor, maxColor;
	private Image image;
	private Slider slider;

	void Start () {
		image = GetComponent<Image> ();
		slider = transform.parent.transform.parent.GetComponent<Slider> ();
	}

	void Update () {
		image.color = Color.Lerp (minColor, maxColor, slider.value);
	}
}
