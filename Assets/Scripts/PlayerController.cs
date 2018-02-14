using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private bool facingRight = true;
	private bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	public Transform groundCheck;
	private bool isGrounded;
	
	private Animator animator;
	private Rigidbody2D rigidbody2D;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		animator.SetBool("Grounded", isGrounded);

		if(Input.GetButtonDown("Jump") && isGrounded)	
			jump = true;			
	}

	private void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");

		animator.SetFloat("Speed", Mathf.Abs(h));

		if(h * rigidbody2D.velocity.x < maxSpeed)
			rigidbody2D.AddForce(h * moveForce * Vector2.right);

		if(rigidbody2D.velocity.x > maxSpeed)
			rigidbody2D.velocity = new Vector2(maxSpeed * Mathf.Sign(h), rigidbody2D.velocity.y);

		if(Mathf.Abs(h) <= 0.05) rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);

		if((h > 0 && !facingRight) || (h < 0 && facingRight)) Flip();

		if(jump)
		{
			rigidbody2D.AddForce(Vector2.up * jumpForce);
			jump = false;
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
