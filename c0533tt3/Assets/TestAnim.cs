using UnityEngine;
using System.Collections;

public class TestAnim : MonoBehaviour {

	private Animator animator = null;

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		this.animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1"))
		{
//			Debug.Log("Jump!");
			this.animator.SetBool ("IsJumping", true);
		} else if (Input.GetButtonUp ("Fire1"))
		{
//			Debug.Log("Stop jump!");
			this.animator.SetBool ("IsJumping", false);
		}
	}
}
