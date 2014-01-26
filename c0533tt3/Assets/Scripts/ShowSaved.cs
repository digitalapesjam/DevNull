using UnityEngine;
using System.Collections.Generic;

public class ShowSaved: MonoBehaviour {

	public GameObject score;

	void Start () {
		foreach (Sprite sp in PlayerController.collected) {
			GameObject g = new GameObject("saved_" + Random.value);
			g.AddComponent<SpriteRenderer>();
			g.GetComponent<SpriteRenderer>().sprite = sp;
			g.AddComponent<Rigidbody2D>();
			g.AddComponent<PolygonCollider2D>();
			g.GetComponent<Rigidbody2D>().fixedAngle = true;
			g.GetComponent<SpriteRenderer>().sortingOrder = 1;
		};

		score.GetComponent<GUIText>().text = "Contacts Recovered: "+PlayerController.points*100.0f/Map.numberOfItems+"%";

		PlayerController.collected = new List<Sprite>();
		PlayerController.points = 0;
	}
}
