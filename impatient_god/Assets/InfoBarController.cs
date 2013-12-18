using UnityEngine;
using System.Collections;

public class InfoBarController : MonoBehaviour {

	public GUIText infoBar;
	public float fadeRate = 0.2f;

	public string defaultString = "Click on City to create life.";

	Color textColor;

	// Use this for initialization
	void Start () {
		infoBar.text = defaultString;
		textColor = infoBar.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (!infoBar.text.Equals (defaultString)) {
			textColor.a -= fadeRate/60f;
			infoBar.color = textColor;
			if (infoBar.color.a <= 0.01f) {
				infoBar.text = defaultString;
				textColor.a = 1;
				infoBar.color = textColor;
			}
		}
	}

	public void setText(string text) {
		infoBar.text = text;
		textColor.a = 1;
		infoBar.color = textColor;
	}
}
