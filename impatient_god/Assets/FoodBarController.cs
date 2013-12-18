using UnityEngine;
using System.Collections;

public class FoodBarController : MonoBehaviour {

	int foodCount = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int useFood() {
		foodCount = Mathf.Max (0, --foodCount);
		return foodCount;
	}
}
