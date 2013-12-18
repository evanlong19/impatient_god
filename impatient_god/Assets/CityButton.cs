using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityButton : MonoBehaviour {

	public FoodBarController foodBar;
	public GameObject cityFolkPrefab;

	Bounds cityBounds;
	List<GameObject> folks = new List<GameObject>();
	int nFolks;

	// Use this for initialization
	void Start () {
		cityBounds = GetComponent<SpriteRenderer> ().sprite.bounds;
		if (PlayerPrefs.HasKey ("foodCount")) {
			nFolks = PlayerPrefs.GetInt ("nFolks");
			int nFolksIter = 0;
			while (nFolksIter < nFolks){
				addFolk (nFolksIter);
				nFolksIter++;
			}
		} else {
			nFolks = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		moveFolks ();
	}

	void OnMouseUp () {
		print ("clicked!");
		if (foodBar.useFood ()) {
			print ("New citizen!");
			addFolk(nFolks);
			nFolks++;
		} else {
			print ("Not enough food!");
		}
	}

	void addFolk (int folkPos){
		GameObject newFolk = (GameObject)Instantiate (cityFolkPrefab);
		float newX = Random.Range (cityBounds.min.x, cityBounds.max.x);
		float newY = Random.Range (cityBounds.min.y, cityBounds.max.y);
		newFolk.transform.Translate (newX, newY, 0);
		print (newFolk.transform.position);
		folks.Add(newFolk);
	}

	void moveFolks(){
		if (folks.Count > 0) {
			int counter = 0;
			while (counter < folks.Count){
				GameObject folk = folks[counter];
				print (folk.transform.position);
				folk.transform.Translate (0.05F,0,0);
				folks[counter] = folk;
				counter++;
			}
		}
	}

	void OnApplicationQuit() {
		PlayerPrefs.SetInt ("nFolks", nFolks);
	}
}
