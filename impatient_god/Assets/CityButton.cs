using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityButton : MonoBehaviour {

	private static readonly System.DateTime UnixEpoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

	public static long minDeathInterval =5;
	public static float maxDeathInterval = 10;
	public static float twinProbability = 0.25f;
	public static Color deadColor = Color.gray;

	public FoodBarController foodBar;
	public GameObject cityFolkPrefab;
	public InfoBarController infoBar;
	public GUIText cityCounter;

	Vector2 cityBounds;
	List<GameObject> folks = new List<GameObject>();
	public List<char> folkDirections = new List<char>();
	int nFolks;
	long totalPlayTime;
	long lastDeathTime;
	long nextDeathTime;
	
	int livingFolks = 0;

	// Use this for initialization
	void Start () {
		cityBounds = GetComponent<BoxCollider2D> ().size;
		if (PlayerPrefs.HasKey ("nFolks")) {
			nFolks = PlayerPrefs.GetInt ("nFolks");
			print (nFolks);
			int nFolksIter = 0;
			while (nFolksIter < nFolks){
				addFolk ();
				nFolksIter++;
			}
		} else {
			nFolks = 0;
		}
		changeDirection ();
		lastDeathTime = GetCurrentUnixTimestampSeconds();	
		nextDeathTime = lastDeathTime + (long)Random.Range(minDeathInterval, maxDeathInterval);
	}

	// Update is called once per frame
	void Update () {
		moveFolks ();
		if (GetCurrentUnixTimestampSeconds() > nextDeathTime) {
			//Kill a folk!
			foreach (GameObject folk in folks) {
				if (folk == null) continue;
				if (folk.GetComponent<SpriteRenderer>().color != deadColor) {
					folk.GetComponent<SpriteRenderer>().color = deadColor;
					livingFolks--;
					cityCounter.text = livingFolks.ToString();
					infoBar.setText("A folk has died!");
					break;
				}
			}
			lastDeathTime  = GetCurrentUnixTimestampSeconds();
			nextDeathTime = lastDeathTime + (long)Random.Range(minDeathInterval, maxDeathInterval);

		}
	}

	void OnMouseUp () {
		if (foodBar.useFood ()) {
			//print ("New folk!");
			if (Random.value < twinProbability) {
				infoBar.setText ("Twins were born!!");
				addFolk();
				nFolks++;
				livingFolks++;
			} else {
				infoBar.setText ("A new city folk is born!");
			}
			addFolk();
			livingFolks++;
		} else {
			infoBar.setText("Can't create more life, not enough food");
		}
		cityCounter.text = livingFolks.ToString ();
	}



	void addFolk (){
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

	void onApplicationQuit() {
		totalPlayTime += (long)Time.realtimeSinceStartup;
		PlayerPrefs.SetInt ("nFolks", nFolks);
	}

	public static long GetCurrentUnixTimestampSeconds()
	{
		return (long) (System.DateTime.UtcNow - UnixEpoch).TotalSeconds;
	}
}
