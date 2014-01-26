using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	bool loading = false;

	void OnGUI () {
		//		Debug.Log ("Doing GUI");
		// Make a background box
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		
		Rect r = new Rect(Screen.width/2 - Screen.width/6,Screen.height/2 - Screen.width/14,Screen.width/3,Screen.width/6);
		
		if(loading) {
			GUI.Box(r, "<size=40>Loading...</size>");
		} else {
			
			if(GUI.Button(r, "<size=40>Retry!</size>")) {
				Debug.Log("Loading Menu");
				loading = true;
				Application.LoadLevel("Menu");
			}
		}
	}
}
