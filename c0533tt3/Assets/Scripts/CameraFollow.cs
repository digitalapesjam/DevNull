using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
//	void Awake () {
//		player = GameObject.FindGameObjectWithTag ("Player").transform;	
//	}

	public GameObject Map;

	// Update is called once per frame
	void Update () {
		float rightLimit = Map.GetComponent<Map>().LevelLength-21;


		Camera.main.transform.position = new Vector3(transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z);

		if (Camera.main.transform.position.x < 0)
			Camera.main.transform.position = new Vector3(0,Camera.main.transform.position.y,Camera.main.transform.position.z);

		if (Camera.main.transform.position.x > rightLimit)
			Camera.main.transform.position = new Vector3(rightLimit,Camera.main.transform.position.y,Camera.main.transform.position.z);
	}
}
