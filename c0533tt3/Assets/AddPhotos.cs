﻿using UnityEngine;
using System.Collections.Generic;

public class AddPhotos : MonoBehaviour {

	private int index = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	void FBPicsLoaded(List<Texture2D> textures) {
		Debug.Log ("Creating sprites");
		textures.ForEach (texture => {
			Sprite s = Sprite.Create(texture,Rect.MinMaxRect(0,0,texture.width,texture.height),Vector2.one/2,texture.width);
			GameObject g = new GameObject("profile_" + index);
			g.AddComponent<SpriteRenderer>();
			g.GetComponent<SpriteRenderer>().sprite = s;
			g.AddComponent<Rigidbody2D>();
			g.AddComponent<PolygonCollider2D>();
			g.GetComponent<Rigidbody2D>().fixedAngle = true;
			g.GetComponent<SpriteRenderer>().sortingOrder = 1;
		});
	}
}
