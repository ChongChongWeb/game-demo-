using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerC : MonoBehaviour
{
	public float runSpeed;
	public float jumpSpeed;
	private BoxCollider2D myFeet;
	private bool isGround;

	private Rigidbody2D myRigidbody;

	// Start is called before the first frame update
	void Start()
	{
		myRigidbody = GetComponent<Rigidbody2D>();
		myFeet = GetComponent<BoxCollider2D>();


	}

	// Update is called once per frame
	void Update()
	{
		Run();
		Jump();
		CheckGrounded();

	}

	void CheckGrounded()
	{
		isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ;
	}
	void Run()
	{
		float moveDir = Input.GetAxis("Horizontal");
		Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
		myRigidbody.velocity = playerVel;

	}
	void Jump() {
		if (Input.GetButtonDown("Jump"))
        {
			if (isGround)
			{
				Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
				myRigidbody.velocity = Vector2.up * jumpVel;
			}
		}
	}
}
