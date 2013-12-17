using UnityEngine;
using System.Collections;

public class CityButton : MonoBehaviour {

	public FoodBarController foodBar;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp () {
		print ("clicked!");
		if (foodBar.useFood () > 0) {
			print ("New citizen!");
		} else {
			print ("Not enough food!");
		}
	}
}
