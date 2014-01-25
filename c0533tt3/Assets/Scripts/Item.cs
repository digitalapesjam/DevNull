using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool IsEnemy;
	public GameObject player;

	// Use this for initialization
	void Start () {
		Debug.Log ("Item behaviour started - isEnemy? " + IsEnemy + " - player tag: " + player.tag);
	}

	void OnCollisionEnter2D(Collision2D collider) {
		Debug.Log ("Entered in a collision " + collider.gameObject.tag);
		if (collider.gameObject == player) {
			// Player hit
			Debug.Log ("Player HIT!");
			Destroy (this.gameObject);

			if (IsEnemy) {
				// somehow decrease the player life
				player.GetComponent<Animator> ().SetTrigger("HurtTrigger");
			} else {
				player.GetComponent<Animator> ().SetTrigger("DrinkTrigger");
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
