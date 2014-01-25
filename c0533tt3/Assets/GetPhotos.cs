using UnityEngine;
using System.Collections.Generic;
using System;

public class GetPhotos : MonoBehaviour {

	private List<Texture2D> photos;

	public GameObject notifier;

	// Use this for initialization
	void Start () {
		photos = new List<Texture2D> ();
		Application.ExternalEval (
@"
var u = new UnityObject2();
u.initPlugin(document.getElementById('unityPlayer'), 'devnull_game');

console.log('About to ask for pretty pictures',u);
FB.api('/me/friends', {fields: 'picture', limit: 30},
	function(response) {
		console.log('Got friends', response);
		if( !response.error ){
			response.data.each(function(friend) {
				var unity = u.getUnity();
				unity.SendMessage('FBHandler', 'AddFriend', friend.picture.data.url);
			});
		}
	});

");
	}

	void AddFriend ( string friendPicUrl ) {
		Debug.Log("got image: " + friendPicUrl);
		MakeTexture2D (friendPicUrl);
	}

	void MakeTexture2D( string fromUrl ) {
		StartCoroutine(LoadTexture(fromUrl,texture => {
			photos.Add (texture);
			if (photos.Count == 30) {
				notifier.SendMessage("FBPicsLoaded", photos);
			}
		}));
	}

	System.Collections.IEnumerator LoadTexture(string url,Action<Texture2D> useTexture) {
		WWW www = new WWW(url); 
		yield return www;
		useTexture(www.texture);
	}
}
