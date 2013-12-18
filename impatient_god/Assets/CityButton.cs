using UnityEngine;
using System.Collections;

public class CityButton : MonoBehaviour {

	public FoodBarController foodBar;
	public GameObject cityFolkPrefab;

	Bounds cityBounds;
	GameObject[] folks;
	int nFolks = 0;

	// Use this for initialization
	void Start () {
		cityBounds = GetComponent<SpriteRenderer> ().sprite.bounds;
		print (cityBounds);
		folks = new GameObject[64];
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp () {
		print ("clicked!");
		if (foodBar.useFood ()) {
			print ("New citizen!");
			GameObject newFolk = (GameObject)Instantiate(cityFolkPrefab);
			float newX = Random.Range(cityBounds.min.x, cityBounds.max.x);
			float newY = Random.Range(cityBounds.min.y, cityBounds.max.y);
			newFolk.transform.Translate(newX, newY, 0);
			print (newFolk.transform.position);
			folks[nFolks] = newFolk;
			nFolks++;
		} else {
			print ("Not enough food!");
		}
	}
}
