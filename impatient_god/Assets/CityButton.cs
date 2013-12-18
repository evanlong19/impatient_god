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
			Vector3 newPosition = new Vector3(Random.Range(-cityBounds.x/2, cityBounds.x/2), 
			                                  Random.Range(-cityBounds.y/2, cityBounds.y/2), 
			                                  transform.position.z);
			GameObject newFolk = (GameObject)Instantiate(cityFolkPrefab, newPosition, Quaternion.identity);
			print (newFolk.transform.position);
			folks[nFolks] = newFolk;
			nFolks++;
		} else {
			print ("Not enough food!");
		}
	}
}
