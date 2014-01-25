using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FollowPlayer : MonoBehaviour {

	public Vector3 offset;

	public Transform player;

//	void Awake () {
//		player = GameObject.FindGameObjectWithTag ("Player").transform;	
//	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.position.x + offset.x,offset.y,player.position.z + offset.z);
	}
}
