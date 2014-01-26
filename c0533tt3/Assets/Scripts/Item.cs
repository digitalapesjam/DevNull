using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public int hpModifier;
	public int ptModifier;
	public GameObject Player;

	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject == Player) {
			// Player hit
			Destroy (this.gameObject);
			Player.GetComponent<PlayerController> ().OnItemCollision(this);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
