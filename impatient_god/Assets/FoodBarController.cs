using UnityEngine;
using System.Collections;

public class FoodBarController : MonoBehaviour {

	private static readonly System.DateTime UnixEpoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

	public static int maxFood = 5;
	public static long foodInterval = 5; //seconds

	int foodCount = 5;
	long lastFoodTime = GetCurrentUnixTimestampSeconds(); //seconds of UNIX time
	long nextFoodTime; //seconds of UNIX time

	public Sprite[] sprites = new Sprite[maxFood + 1];

	public SpriteRenderer render;
	public GUIText foodTimer;

	// Use this for initialization
	void Start () {
		foodCount += (int)((GetCurrentUnixTimestampSeconds () - lastFoodTime) / foodInterval);
		foodCount = Mathf.Min (maxFood, foodCount);
		lastFoodTime = GetCurrentUnixTimestampSeconds () - (GetCurrentUnixTimestampSeconds () - lastFoodTime) % foodInterval;
		nextFoodTime = lastFoodTime + foodInterval;
		render.sprite = sprites[foodCount];
	}
	
	// Update is called once per frame
	void Update () {
		if (GetCurrentUnixTimestampSeconds() > nextFoodTime) {
			//Add food!
			foodCount = Mathf.Min(foodCount + 1, maxFood);
			render.sprite = sprites[foodCount];
			lastFoodTime = GetCurrentUnixTimestampSeconds();
			nextFoodTime = lastFoodTime + foodInterval;
		}
		long timeToFood = nextFoodTime - GetCurrentUnixTimestampSeconds ();
		foodTimer.text = "Next food in " + timeToFood/3600 + "h " + (timeToFood%3600)/60 + "m " + (timeToFood%60 + 1) + "s";
	}

	public bool useFood() {
		bool hasFood = foodCount > 0;
		foodCount = Mathf.Max (0, --foodCount);
		if (foodCount <= maxFood) {
			render.sprite = sprites[foodCount];
		}
		return hasFood;
	}

	public static long GetCurrentUnixTimestampSeconds()
	{
		return (long) (System.DateTime.UtcNow - UnixEpoch).TotalSeconds;
	}
}
