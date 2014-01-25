using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public bool IsEnemy;
	public GameObject Player;

	// Use this for initialization
	void Start () {
		Debug.Log ("Item behaviour started - isEnemy? " + IsEnemy + " - player tag: " + Player.tag);
	}

	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject == Player) {
			// Player hit
			Destroy (this.gameObject);

			if (IsEnemy) {
				// somehow decrease the player life
				Player.GetComponent<PlayerController> ().OnHurt(this);
				Player.GetComponent<Animator> ().SetTrigger("HurtTrigger");
			} else {
				Player.GetComponent<Animator> ().SetTrigger("DrinkTrigger");
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
