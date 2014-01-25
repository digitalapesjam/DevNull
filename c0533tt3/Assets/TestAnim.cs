﻿using UnityEngine;
using System.Collections;

public class TestAnim : MonoBehaviour {

	[HideInInspector]
	public bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	private Animator animator = null;

	private bool grounded;
	private float lastTimeJump = 0f;
	private float lastTimeJumpDelta = 0.1f;

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		this.animator = GetComponent<Animator> ();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log ("CollisionEnter");
		Debug.Log (collision.collider.CompareTag ("Ground"));
		if (collision.collider.CompareTag ("Ground"))
						grounded = true;

	}

	void OnCollisionExit2D(Collision2D collision)
	{
		Debug.Log ("CollisionExit");
		Debug.Log (collision.collider.CompareTag ("Ground"));
		if (collision.collider.CompareTag ("Ground"))
						grounded = false;
	}

	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		if (h * rigidbody2D.velocity.x < maxSpeed)
						rigidbody2D.AddForce (Vector2.right * h * moveForce);

		if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed)
						rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);


		if (Input.GetButtonDown ("Jump") && grounded) {
			Debug.Log("jumping");
			Debug.Log(Time.time);
			animator.SetBool ("IsJumping", true);
			lastTimeJump = Time.time;
			jump = true;
		}
		else if (animator.GetBool("IsJumping") && grounded && ((Time.time - lastTimeJump) > lastTimeJumpDelta)) {
			Debug.Log ("jump ended");
			Debug.Log(Time.time);
			animator.SetBool ("IsJumping", false);
		}
//		if (Input.GetButtonDown ("Jump"))
//		{
//			this.animator.SetBool ("IsJumping", true);
//		} else if (Input.GetButtonUp ("Jump"))
//		{
//			this.animator.SetBool ("IsJumping", false);
//		}
	}

	void FixedUpdate ()
	{
		if (jump) {
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}
}
