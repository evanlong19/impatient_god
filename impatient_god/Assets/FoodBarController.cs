using UnityEngine;
using System.Collections;

public class FoodBarController : MonoBehaviour {

	int foodCount = 4;

	public Sprite[] sprites = new Sprite[6];

	public SpriteRenderer render;

	// Use this for initialization
	void Start () {
		render.sprite = sprites[foodCount];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int useFood() {
		foodCount = Mathf.Max (0, --foodCount);
		if (foodCount < sprites.GetLength (0)) {
			render.sprite = sprites[foodCount];
		}
		return foodCount;
	}
}
