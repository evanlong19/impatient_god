using UnityEngine;
using System.Collections;

public class CityButton : MonoBehaviour {

	private static readonly System.DateTime UnixEpoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

	public static long minDeathInterval =5;
	public static float maxDeathInterval = 10;
	public static float twinProbability = 0.25f;
	public static Color deadColor = Color.gray;

	public FoodBarController foodBar;
	public GameObject cityFolkPrefab;
	public InfoBarController infoBar;

	long lastDeathTime;
	long nextDeathTime;

	Vector2 cityBounds;
	GameObject[] folks;
	int nFolks = 0;

	// Use this for initialization
	void Start () {
		cityBounds = GetComponent<BoxCollider2D> ().size;
		folks = new GameObject[64];
		lastDeathTime = GetCurrentUnixTimestampSeconds();	
		nextDeathTime = lastDeathTime + (long)Random.Range(minDeathInterval, maxDeathInterval);
	}

	// Update is called once per frame
	void Update () {
		if (GetCurrentUnixTimestampSeconds() > nextDeathTime) {
			//Kill a folk!
			foreach (GameObject folk in folks) {
				if (folk.GetComponent<SpriteRenderer>().color != deadColor) {
					folk.GetComponent<SpriteRenderer>().color = deadColor;
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
			Vector3 newPosition;
			GameObject newFolk;
			if (Random.value < twinProbability) {
				infoBar.setText ("Twins were born!!");
				newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				newPosition.x += Random.Range(-cityBounds.x, cityBounds.x);
				newPosition.y += Random.Range(-cityBounds.y, cityBounds.y);
				newFolk = (GameObject)Instantiate(cityFolkPrefab, newPosition, Quaternion.identity);
				folks[nFolks] = newFolk;
				nFolks++;
			} else {
				infoBar.setText ("A new city folk is born!");
			}
			newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			newPosition.x += Random.Range(-cityBounds.x, cityBounds.x);
			newPosition.y += Random.Range(-cityBounds.y, cityBounds.y);
			newFolk = (GameObject)Instantiate(cityFolkPrefab, newPosition, Quaternion.identity);
			folks[nFolks] = newFolk;
			nFolks++;
		} else {
			infoBar.setText("Can't create more life, not enough food");
		}
	}

	public static long GetCurrentUnixTimestampSeconds()
	{
		return (long) (System.DateTime.UtcNow - UnixEpoch).TotalSeconds;
	}
}
