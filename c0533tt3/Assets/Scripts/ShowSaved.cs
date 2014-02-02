using UnityEngine;
using System.Collections.Generic;

public class ShowSaved: MonoBehaviour {

	public GameObject score;

	void Start () {
		foreach (Texture2D texture in PlayerController.collected) {
			Sprite sp = Sprite.Create(texture,Rect.MinMaxRect(0,0,texture.width,texture.height),Vector2.one/2,texture.width);
			GameObject g = new GameObject("saved_" + Random.value);
			g.AddComponent<SpriteRenderer>();
			g.GetComponent<SpriteRenderer>().sprite = sp;
			g.AddComponent<Rigidbody2D>();
			g.AddComponent<PolygonCollider2D>();
			g.GetComponent<Rigidbody2D>().fixedAngle = true;
			g.GetComponent<SpriteRenderer>().sortingOrder = 1;
		};

		score.GetComponent<GUIText>().text = "Contacts Recovered: "+PlayerController.points*100.0f/Map.numberOfItems+"%";

		PlayerController.collected = new List<Texture2D>();
		PlayerController.points = 0;
	}
}
