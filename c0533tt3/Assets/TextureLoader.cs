using UnityEngine;
using System.Collections;
using System;

public class TextureLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadTexture("",texture => {
			Sprite s = Sprite.Create(texture,Rect.MinMaxRect(0,0,texture.width,texture.height),Vector2.one/2,texture.width);
			GameObject g = new GameObject("whatever");
			g.AddComponent<SpriteRenderer>();
			g.GetComponent<SpriteRenderer>().sprite = s;
			g.AddComponent<Rigidbody2D>();
			g.AddComponent<PolygonCollider2D>();
			g.GetComponent<Rigidbody2D>().fixedAngle = true;
			g.GetComponent<SpriteRenderer>().sortingOrder = 1;
		}));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator LoadTexture(string url,Action<Texture2D> useTexture) {
		WWW www = new WWW(url); 
		yield return www;
		useTexture(www.texture);
	}
}
