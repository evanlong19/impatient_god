using UnityEngine;
using System.Collections;

public class CityButton : MonoBehaviour {

	public static System.TimeSpan deathInterval = System.TimeSpan.FromSeconds(7);
	public static Color deadColor = Color.gray;

	public FoodBarController foodBar;
	public GameObject cityFolkPrefab;

	System.DateTime lastDeathTime;
	System.DateTime nextDeathTime;

	Vector2 cityBounds;
	GameObject[] folks;
	int nFolks = 0;

	// Use this for initialization
	void Start () {
		cityBounds = GetComponent<BoxCollider2D> ().size;
		folks = new GameObject[64];
		lastDeathTime = System.DateTime.Now;
		nextDeathTime = lastDeathTime.Add (deathInterval);
		//TODO: Randomize death Interval
	}

	// Update is called once per frame
	void Update () {
		if (System.DateTime.Now.CompareTo (nextDeathTime) > 0) {
			//Kill a folk!
			foreach (GameObject folk in folks) {
				if (folk.GetComponent<SpriteRenderer>().color != deadColor) {
					folk.GetComponent<SpriteRenderer>().color = deadColor;
					break;
				}
			}
			lastDeathTime = System.DateTime.Now;
			nextDeathTime = lastDeathTime.Add (deathInterval);
		}
	}

	void OnMouseUp () {
		print ("clicked!");
		if (foodBar.useFood ()) {
			print ("New folk!");
			Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			newPosition.x += Random.Range(-cityBounds.x, cityBounds.x);
			newPosition.y += Random.Range(-cityBounds.y, cityBounds.y);
			GameObject newFolk = (GameObject)Instantiate(cityFolkPrefab, newPosition, Quaternion.identity);
			print (newFolk.transform.position);
			folks[nFolks] = newFolk;
			nFolks++;
		} else {
			print ("Not enough food!");
		}
	}
}
