using UnityEngine;
using System.Collections.Generic;

public class LifesHandler : MonoBehaviour {

	public Texture2D lifePic;
	FbPicturesHolder dataHolder;

	List<Heart> hearts = new List<Heart>();

	// Use this for initialization
	void Start () {
		Debug.Log ("Initializing life handler");
		// re initialize lives & collected
		dataHolder = GameObject.FindObjectOfType<FbPicturesHolder> ();
		PlayerController.collected.Clear ();
		PlayerController.points = 0;
		dataHolder.Lifes = 2;

		Heart.texture = lifePic;

		hearts.Add (new Heart (0));
		hearts.Add (new Heart (1));
		hearts.Add (new Heart (2));
	}

	void OnGUI () {
//		GUIStyle style = new GUIStyle ();
//		style.normal.textColor = Color.white;
//		style.contentOffset = new Vector2 (0, 0);
//		GUI.Label (new Rect(5,2,5,2), "" + (dataHolder.Lifes + 1), style);

		foreach (Heart h in hearts) {
			h.OnGui();
		}
		hearts.RemoveAll (h => h.dead);
		if (hearts.Count == 0) {
			Debug.Log ("Player dead");
			Application.LoadLevel ("GameOver");
		}
	}

	public void OnLifeLost () {
		dataHolder.Lifes -= 1;
		Debug.Log ("Hurted - lifes: " + dataHolder.Lifes);
		hearts [hearts.Count - 1].play = true;
	}

	/*dafuk? no animated stuff in the GUI framework?*/
	private class Heart {
		public static Texture2D texture;
		static int totalFrames = 22;
		static int side = 46;
		int currentFrame = 0;
		Rect screen;
		Rect frame;
		public bool play = false;
		public bool dead = false;

		public Heart(int index) {
			screen = new Rect(index * side, 2, side, side);
			frame = new Rect(0, 0, Heart.texture.width, Heart.texture.height);
		}

		void nextFrame() {
			currentFrame = (currentFrame + 1);
			frame.x = - (side * currentFrame);
			dead = currentFrame == totalFrames;
		}

		public void OnGui() {
			if (dead) {return;}
			if (play) {nextFrame ();}
			GUI.BeginGroup (screen);
			GUI.DrawTexture (frame, Heart.texture);
			GUI.EndGroup ();
		}
	}
}
