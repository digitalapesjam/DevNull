using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float MaxSpeed=5;
	public float MinSpeed=1;
	bool grounded=false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask groundMask;
	public float jumpForce = 500f;

	private int hurtingAnimFrameMax = 30;
	private int hurtingAnimFrames = -1;
	Animator animator;

	public static int points = 0;
	public static List<Texture2D> collected = new List<Texture2D>();

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		if(hurtingAnimFrames >= 0) {
			if (hurtingAnimFrames == hurtingAnimFrameMax)
				rigidbody2D.AddForce(new Vector2(-jumpForce/2, jumpForce * 2));
			hurtingAnimFrames -= 1;
			return;
		}
		grounded = Physics2D.OverlapCircle(groundCheck.position,groundRadius,groundMask);
		animator.SetBool("IsGrounded",grounded);
		animator.SetFloat("VerticalSpeed",rigidbody2D.velocity.y);
		float move = Input.GetAxis ("Horizontal");
		float speed = 0.5f*(MaxSpeed+MinSpeed) + move*0.5f*(MaxSpeed-MinSpeed);
		rigidbody2D.velocity = new Vector2(speed,rigidbody2D.velocity.y);

		// adjust animation speed depending on player speed
		animator.speed = speed / MaxSpeed;

		if (!grounded && Input.GetKey(KeyCode.Space) && rigidbody2D.velocity.y > 0){
			rigidbody2D.AddForce(new Vector2(0,jumpForce*0.06f));
		}
	}

	void Update() {
		if(grounded && Input.GetKeyDown(KeyCode.Space)){
			animator.SetBool("IsGrounded",false);
			rigidbody2D.AddForce(new Vector2(0,jumpForce*Mathf.Max(1,rigidbody2D.velocity.x/20f)));
		}
	}

	public void OnItemCollision(Item item){
		int oldPts = points;
		points += item.ptModifier;
//		dataHolder.Lives += item.hpModifier;
		Debug.Log ("item collision - new pts: " + points + " - was: " + oldPts);

		if (item.ptModifier > 0) {
			collected.Add ((Texture2D)item.GetComponent<Renderer>().material.mainTexture);
			animator.SetTrigger("DrinkTrigger");
		}

		if (item.hpModifier < 0) {
			hurtingAnimFrames = hurtingAnimFrameMax;
			animator.SetTrigger("HurtTrigger");
			GameObject.FindObjectOfType<LifesHandler> ().OnLifeLost ();
//			Application.LoadLevel (Application.loadedLevelName);
		} else if (item.hpModifier > 0) {
			animator.SetTrigger("DrinkTrigger");
		}
//		if (dataHolder.Lives < 0) {
			// player dead
//		}
	}
}
