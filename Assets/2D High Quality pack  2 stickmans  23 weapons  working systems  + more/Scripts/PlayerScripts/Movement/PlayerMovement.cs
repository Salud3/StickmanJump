using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public PlayerController controller;
	public Animator animator;
	public float walkSpeed = 40f;
	public float runSpeedModifier = 2f;
	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	bool running = false;


	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if(running)
		{
			horizontalMove*= runSpeedModifier;
		}


		
		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("Jump", true);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			running = true;
		}

		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			running = false;
		}
	}

	public void OnLanding ()
	{
		animator.SetBool("Jump", false);
	}

	public void OnCrouching (bool crouch)
	{
		animator.SetBool("Crouch", crouch);
	}



	void FixedUpdate ()
	{
		
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}