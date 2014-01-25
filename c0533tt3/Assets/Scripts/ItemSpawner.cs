using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemSpawner : MonoBehaviour {

	public GameObject Player;
	public Texture2D[] BadItemTextures;
	public Texture2D[] GoodItemTextures;

	void FBPicsLoaded(List<Texture2D> textures) {
		GoodItemTextures = textures.ToArray ();
	}

	void SpawnItems(int bad, int good, Action<GameObject> positioner) {
		int totalBad = BadItemTextures.Length;
		int totalGood = GoodItemTextures.Length;

		for(int i = 0; i < bad; i++) {
			Texture2D texture = BadItemTextures[i % totalBad];
			positioner( spawnItem(i, true, texture) );
		}

		for(int i = 0; i < good; i++) {
			Texture2D texture = GoodItemTextures[i % totalGood];
			positioner( spawnItem(i, false, texture) );
		}
	}

	private GameObject spawnItem(int index, bool isEnemy, Texture2D texture) {
		Sprite s = Sprite.Create(texture,Rect.MinMaxRect(0,0,texture.width,texture.height),Vector2.one/2,texture.width);
		GameObject g = new GameObject("profile_" + index);
		g.AddComponent<SpriteRenderer>();
		g.GetComponent<SpriteRenderer>().sprite = s;
		g.AddComponent<Rigidbody2D>();
		g.AddComponent<PolygonCollider2D>();
		g.GetComponent<Rigidbody2D>().fixedAngle = true;
		g.GetComponent<SpriteRenderer>().sortingOrder = 1;
		g.AddComponent<Item>();
		Item item = g.GetComponent<Item>();
		item.IsEnemy = isEnemy;
		item.Player = this.Player;
		return g;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
