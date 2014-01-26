using UnityEngine;
using System.Collections;

public class FbPicturesHolder : MonoBehaviour {

	public Texture2D[] FbTextures;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
