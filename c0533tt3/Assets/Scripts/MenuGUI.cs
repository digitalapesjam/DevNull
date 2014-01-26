using UnityEngine;
using System.Collections.Generic;

public class MenuGUI : MonoBehaviour {

	void OnGUI () {
//		Debug.Log ("Doing GUI");
		// Make a background box
		GUI.Box(new Rect(10,10,100,90), "Loader Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
			Debug.Log("Loading Photos");
			GetComponent<GetPhotos> ().FetchURLs();
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
