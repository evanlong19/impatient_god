using UnityEngine;
using System.Collections;

public class FoodBarController : MonoBehaviour {

	public static int maxFood = 5;
	public static System.TimeSpan foodInterval = System.TimeSpan.FromSeconds(5);

	int foodCount = 5;
	System.DateTime lastFoodTime;
	System.DateTime nextFoodTime;

	public Sprite[] sprites = new Sprite[maxFood + 1];

	public SpriteRenderer render;

	// Use this for initialization
	void Start () {
		render.sprite = sprites[foodCount];
		lastFoodTime = System.DateTime.Now;
		nextFoodTime = lastFoodTime.Add (foodInterval);
	}
	
	// Update is called once per frame
	void Update () {
		if (System.DateTime.Now.CompareTo (nextFoodTime) > 0) {
			//Add food!
			foodCount = Mathf.Min(foodCount + 1, maxFood);
			render.sprite = sprites[foodCount];
			lastFoodTime = System.DateTime.Now;
			nextFoodTime = lastFoodTime.Add (foodInterval);
		}
	}

	public bool useFood() {
		bool hasFood = foodCount > 0;
		foodCount = Mathf.Max (0, --foodCount);
		if (foodCount <= maxFood) {
			render.sprite = sprites[foodCount];
		}
		return hasFood;
	}
}
