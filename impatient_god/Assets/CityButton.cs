using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityButton : MonoBehaviour {
	public static System.TimeSpan deathInterval = System.TimeSpan.FromSeconds(7);
	public static Color deadColor = Color.gray;

	public FoodBarController foodBar;
	public GameObject cityFolkPrefab;

	Vector2 cityBounds;
	List<GameObject> folks = new List<GameObject>();
	public List<char> folkDirections = new List<char>();
	int nFolks;
	System.DateTime lastDeathTime;
	System.DateTime nextDeathTime;

	// Use this for initialization
	void Start () {
		cityBounds = GetComponent<BoxCollider2D> ().size;
		if (PlayerPrefs.HasKey ("nFolks")) {
			nFolks = PlayerPrefs.GetInt ("nFolks");
			print (nFolks);
			int nFolksIter = 0;
			while (nFolksIter < nFolks){
				addFolk (nFolksIter);
				nFolksIter++;
			}
		} else {
			nFolks = 0;
		}
		changeDirection ();
		lastDeathTime = System.DateTime.Now;
		nextDeathTime = lastDeathTime.Add (deathInterval);
		//TODO: Randomize death Interval
	}

	// Update is called once per frame
	void Update () {

		moveFolks ();
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
			addFolk(nFolks);
			//print ("New folk!");
			nFolks++;
		} else {
			//print ("Not enough food!");
		}
	}



	void addFolk (int folkPos){
		Vector3 newPosition = new Vector3(-1, -1, transform.position.z);
		GameObject newFolk = (GameObject)Instantiate(cityFolkPrefab, newPosition, Quaternion.identity);
		//print (newFolk.transform.position);
		folks.Add(newFolk);
		folkDirections.Add (randDirection ());
	}

	public char randDirection (){
				int direction = Random.Range (0, 4);
				if (direction >= 0 && direction < 1) {
						return 'N';
				} else if (direction >= 1 && direction < 2) {
						return 'S';
				} else if (direction >= 2 && direction < 3) {
						return 'E';
				} else {
						return 'W';
				}
		}

	public void changeDirection(){
		if (folks.Count > 0) {
			int counter = 0;
			while (counter < folkDirections.Count){
				folkDirections[counter] = randDirection ();
				counter++;
			}
		}
	}

	void moveFolks(){
		if (folks.Count > 0) {
			int counter = 0;
			while (counter < folks.Count){
				GameObject folk = folks[counter];
				char rand = randDirection ();
				folkDirections = folk.GetComponent<FolkMovement>().updateFolkDirections(counter, folkDirections, rand);
				char moveDirection = folkDirections[counter];
				if (moveDirection == 'N'){
					folk.transform.Translate (0,0.02F,0);
				} else if (moveDirection == 'S'){
					folk.transform.Translate (0,-0.02F,0);
				}else if (moveDirection == 'E'){
					folk.transform.Translate (0.02F,0,0);
				}else{
					folk.transform.Translate (-0.02F,0,0);
				}
				folks[counter] = folk;
				counter++;
			}
		}
	}

	void OnApplicationQuit() {
		print ("exit");
		PlayerPrefs.SetInt ("nFolks", nFolks);
	}
}
