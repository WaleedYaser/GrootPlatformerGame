using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private bool facingRight = true;
	public bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	public Transform groundCheck;
	private bool isGrounded;
	
	private Animator animator;
	private Rigidbody2D rigidbody2D;

	public ParticleSystem forceJumpEffect;
	public ParticleSystem moveParticle;

	public float bounceFactor = 1.25f;
	public float forceJumpLimit = 1700f;
	public float HorizontalJumpFactor = 100f;

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

		if(Mathf.Abs(h * rigidbody2D.velocity.x) < maxSpeed)
			rigidbody2D.AddForce(h * moveForce * Vector2.right);

		if(Mathf.Abs(h) <= 0.05) rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		else if(isGrounded) moveParticle.Play();	

		if((h > 0 && !facingRight) || (h < 0 && facingRight)) Flip();

		if(jump)
		{
			float totalJumpForce = jumpForce + Mathf.Abs(rigidbody2D.velocity.x) * HorizontalJumpFactor;
			if(totalJumpForce > forceJumpLimit)
				forceJumpEffect.Play();
			rigidbody2D.AddForce(Vector2.up * totalJumpForce);
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

	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Wall")
		{
			Flip();
			Vector2 rev = new Vector2(rigidbody2D.velocity.x * bounceFactor, 0);
			rigidbody2D.AddForce(rev, ForceMode2D.Impulse);
		}
	}
}
