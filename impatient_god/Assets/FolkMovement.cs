using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FolkMovement : MonoBehaviour {
	bool collisionCheck = false;

	void OnCollisionEnter2D(){
		collisionCheck = true;
	}

	void OnCollisionStay2D(){
		collisionCheck = true;
	}

	public List<char> updateFolkDirections(int i, List<char> folkDirections, char randDirection){
		if (collisionCheck) {
			print (randDirection);
			folkDirections [i] = randDirection;
			collisionCheck = false;
			return folkDirections;
		} else {
			return folkDirections;
		}
	}
}

