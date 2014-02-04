using UnityEngine;
using System.Collections.Generic;
using System;

public class GetPhotos : MonoBehaviour {

	private List<Texture2D> photos;

	public GameObject notifier;

	public Texture2D[] testPhotos;

	private int ImageCount = 30;
	private bool isTest = false;

	void Awake () {
		photos = new List<Texture2D> ();
	}

	public void FetchURLs()
	{
		if(Application.isWebPlayer) {
			Application.ExternalEval (
				@"
console.log('sending urls to Unity', window.fbf_urls);
u.getUnity().SendMessage('FBHandler', 'AddFriends', window.fbf_urls);
");
		} else {
			isTest = true;

			for(int i = 0; i < ImageCount; i++) {
				photos.Add (testPhotos[i % testPhotos.Length]);
			}

			notifier.SendMessage("FBPicsLoaded", photos);

//			AddFriend ("https://fbcdn-profile-a.akamaihd.net/hprofile-ak-ash1/t5/275330_609928551_792317627_q.jpg");
//			AddFriend ("https://fbcdn-profile-a.akamaihd.net/hprofile-ak-prn2/273836_601768550_1429033367_q.jpg");
//			AddFriend ("https://fbcdn-profile-a.akamaihd.net/hprofile-ak-prn1/48895_595715443_4117_q.jpg");
//			AddFriend ("https://fbcdn-profile-a.akamaihd.net/hprofile-ak-prn1/t5/41657_583738753_8955_q.jpg");
//			AddFriend ("https://fbcdn-profile-a.akamaihd.net/hprofile-ak-ash1/t5/371129_577322690_159507207_q.jpg");
		}
	}

	void ExternalLog (string o) {
		Application.ExternalEval ("console.log('UNITY','" + o + "')");
	}

	void AddFriends (string friendPicUrls) {
		foreach (string s in friendPicUrls.Split (' ')) {
			AddFriend (s);
		}
	}
	void AddFriend ( string friendPicUrl ) {
		ExternalLog("got image: " + friendPicUrl);
		MakeTexture2D (friendPicUrl);
	}

	void MakeTexture2D( string fromUrl ) {
		StartCoroutine(LoadTexture(fromUrl,texture => {
			ExternalLog(fromUrl + " => texture created");
			photos.Add (texture);
			if (photos.Count == ImageCount) {
				ExternalLog("FBPics Loaded!");
				if(isTest) {
					// add moaaaaar photos
					int toAdd = 30 - photos.Count;
					for(int i = 0; i < toAdd; i++) {
						photos.Add (photos[i % 5]);
					}
				}
				notifier.SendMessage("FBPicsLoaded", photos);

			}
		}));
	}

	System.Collections.IEnumerator LoadTexture(string url,Action<Texture2D> useTexture) {
		ExternalLog ("Get picture...");
		WWW www = new WWW(url); 
		yield return www;
		ExternalLog ("Got it!");
		useTexture(www.texture);
	}
}
