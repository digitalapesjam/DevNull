using UnityEngine;
using System.Collections;

public class LevelComplete : MonoBehaviour {

	public GameObject player;

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.gameObject == player) {
			Application.LoadLevel("Score");
		}
	}
}
