using UnityEngine;
using System.Collections;

public class FoodBarController : MonoBehaviour {

	int foodCount = 5;

	public Texture2D food0of5tex;
	public Texture2D food1of5tex;
	public Texture2D food2of5tex;
	public Texture2D food3of5tex;
	public Texture2D food4of5tex;
	public Texture2D food5of5tex;
	
	Texture2D[] texs = new Texture2D[6];

	public SpriteRenderer render;

	Sprite currentSprite;

	// Use this for initialization
	void Start () {
		texs [0] = food0of5tex;
		texs [1] = food1of5tex;
		texs [2] = food2of5tex;
		texs [3] = food3of5tex;
		texs [4] = food4of5tex;
		texs [5] = food5of5tex;

		currentSprite = render.sprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int useFood() {
		foodCount = Mathf.Max (0, --foodCount);
		if (foodCount < texs.GetLength (0)) {
			currentSprite.texture = texs[foodCount];
		}
		return foodCount;
	}
}
