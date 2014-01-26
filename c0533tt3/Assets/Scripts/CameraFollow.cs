using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float offset;

//	void Awake () {
//		player = GameObject.FindGameObjectWithTag ("Player").transform;	
//	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = new Vector3(transform.position.x + offset,Camera.main.transform.position.y,Camera.main.transform.position.z);
	}
}
