using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public ScoreManager scoreManager;

	private bool facingRight = true;
	private bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public float bounceFactor = 1.25f;
	public float forceJumpLimit = 1700f;
	public float HorizontalJumpFactor = 100f;

	public ParticleSystem forceJumpEffect;
	public ParticleSystem moveParticle;

	public Transform groundCheck;
	private bool isGrounded;
	
	private Animator animator;
	private Rigidbody2D rb2D;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
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

		if(Mathf.Abs(h * rb2D.velocity.x) < maxSpeed)
			rb2D.AddForce(h * moveForce * Vector2.right);

		if(Mathf.Abs(h) <= 0.05) rb2D.velocity = new Vector2(0, rb2D.velocity.y);
		else if(isGrounded) moveParticle.Play();
		else moveParticle.Stop();

		if((h > 0 && !facingRight) || (h < 0 && facingRight)) Flip();

		if(jump)
		{
			float totalJumpForce = jumpForce + Mathf.Abs(rb2D.velocity.x) * HorizontalJumpFactor;
			if(totalJumpForce > forceJumpLimit)
				forceJumpEffect.Play();
			rb2D.AddForce(Vector2.up * totalJumpForce);
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
			Vector2 rev = new Vector2(rb2D.velocity.x * bounceFactor, 0);
			rb2D.AddForce(rev, ForceMode2D.Impulse);
		}
		if(col.gameObject.tag == "Platform")
		{
			scoreManager.UpdateScore((int)transform.position.y);
		}
	}
}
