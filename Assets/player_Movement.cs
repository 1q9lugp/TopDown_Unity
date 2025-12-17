using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 5f;
	private Rigidbody2D rb;
	private Vector2 moveInput;
	private Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		rb.linearVelocity = moveInput * moveSpeed;
	}

	public void Move(InputAction.CallbackContext context)
	{
		// Read the current input value
		moveInput = context.ReadValue<Vector2>();

		// Set walking animation state
		if (context.canceled)
		{
			animator.SetBool("isWalking", false);
		}
		else
		{
			animator.SetBool("isWalking", true);
		}

		// Update last input and current input values for the animator
		animator.SetFloat("LastInputX", moveInput.x);
		animator.SetFloat("LastInputY", moveInput.y);
		animator.SetFloat("InputX", moveInput.x);
		animator.SetFloat("InputY", moveInput.y);
	}
}


