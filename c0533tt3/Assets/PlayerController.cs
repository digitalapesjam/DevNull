﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float MaxSpeed=5;
	public float MinSpeed=1;
	bool grounded=false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask groundMask;
	public float jumpForce = 500f;

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius,groundMask);
		animator.SetBool("IsGrounded",grounded);
		animator.SetFloat("VerticalSpeed",rigidbody2D.velocity.y);
		float move = Input.GetAxis ("Horizontal");
		float speed = 0.5f*(MaxSpeed+MinSpeed) + move*0.5f*(MaxSpeed-MinSpeed);
		rigidbody2D.velocity = new Vector2(speed,rigidbody2D.velocity.y);
	}

	void Update() {
		 if(grounded && Input.GetKeyDown(KeyCode.UpArrow)){
			animator.SetBool("IsGrounded",false);
			rigidbody2D.AddForce(new Vector2(0,jumpForce*Mathf.Max(1,rigidbody2D.velocity.x/10f)));
		}

		if (!grounded && Input.GetKey(KeyCode.UpArrow) && rigidbody2D.velocity.y > 0){
			rigidbody2D.AddForce(new Vector2(0,jumpForce*0.01f));
		}
	}
}
