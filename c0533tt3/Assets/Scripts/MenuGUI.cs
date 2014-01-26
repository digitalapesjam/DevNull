using UnityEngine;
using System.Collections.Generic;

public class MenuGUI : MonoBehaviour {
	
	bool loading = false;
	void OnGUI () {
//		Debug.Log ("Doing GUI");
		// Make a background box

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed

		Rect r = new Rect(Screen.width - Screen.width/3f,Screen.height/4,Screen.width/4,Screen.width/12);

		if(loading) {
			GUI.Box(r, "<size=30>Loading...</size>");
		} else {

				if(GUI.Button(r, "<size=30>Play</size>")) {
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
