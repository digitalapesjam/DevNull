using UnityEngine;
using System.Collections.Generic;

public class MenuGUI : MonoBehaviour {

	bool loading = false;
	void OnGUI () {
//		Debug.Log ("Doing GUI");
		// Make a background box

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed

		if(loading) {
			GUI.Box(new Rect(Screen.width - 200,10,100,90), "Please wait...");
		} else {
			if(GUI.Button(new Rect(Screen.width - 200,40,80,20), "Play!")) {
				Debug.Log("Loading Photos");
				loading = true;
				GetComponent<GetPhotos> ().FetchURLs();
			}
		}
	}

	public void FBPicsLoaded (List<Texture2D> textures) {
		GameObject.FindObjectOfType<FbPicturesHolder> ().FbTextures = textures.ToArray();
		Application.LoadLevel("fbtest");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
