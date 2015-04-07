using UnityEngine;
using System.Collections;

public class PlayerBhv : MonoBehaviour
{
	private Rigidbody2D rb;
	private KeyCode up = KeyCode.UpArrow;
	private KeyCode left = KeyCode.LeftArrow;
	private KeyCode down = KeyCode.DownArrow;
	private KeyCode right = KeyCode.RightArrow;
	private bool jumping;
	private bool walking;
	private bool backingUp;
	private bool crouching;
	private bool isGrounded; // is player on the ground?
	int currentState = CharStateBhv.ST_JUMPING;
	string currentDirection = "left";
	public Animator animator;
	public float walkSpeed = 1; // player left right walk speed
	public int jumpForce = 150;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		animator = transform.GetComponent<Animator> ();
	}
	
	void FixedUpdate ()
	{
		if (isGrounded && Input.GetKeyDown (up)) {
			changeState(CharStateBhv.ST_JUMPING);
			rb.AddForce (new Vector2 (0, jumpForce));
			isGrounded = false;
			//Debug.Log("registering input @"+Time.time);
		} else if (Input.GetKey (right) || Input.GetKey (left)) { 
			if (Input.GetKey (right)) {
				changeState(CharStateBhv.ST_WALKING_F);
			} else if (Input.GetKey (left)) {
				changeState(CharStateBhv.ST_WALKING_B);
			}
		} else if (Input.GetKey (down)) { 
			changeState(CharStateBhv.ST_CROUCHING);
		} else {
			changeState(CharStateBhv.ST_IDLE);
		}
	}

	private void cancelWalk ()
	{
		walking = false;
		backingUp = false;
	}

	private void cancel ()
	{
		cancelWalk ();
		crouching = false;
		jumping = false;
	}

	private void die (string msg)
	{
		Debug.LogError (msg);
		throw new MissingComponentException ();
	}

	private void changeState (int state)
	{
		if (currentState == state)
			return;
		
		switch (state) {
		case CharStateBhv.ST_WALKING_F:
			setState(CharStateBhv.ST_WALKING_F);
			break;
			
		case CharStateBhv.ST_CROUCHING:
			setState ( CharStateBhv.ST_CROUCHING);
			break;
			
		case CharStateBhv.ST_JUMPING:
			setState (CharStateBhv.ST_JUMPING);
			break;
			
		case CharStateBhv.ST_IDLE:
			setState (CharStateBhv.ST_IDLE);
			break;
			
		case CharStateBhv.ST_WALKING_B:
			setState (CharStateBhv.ST_WALKING_B);
			break;
		}
		
		currentState = state;
	}

	private void setState(int state) {
		animator.SetInteger ("state", state);
	}

	void changeDirection (string direction)
	{
		if (currentDirection != direction) {
			if (direction == "right") {
				transform.Rotate (0, 180, 0);
				currentDirection = "right";
			} else if (direction == "left") {
				transform.Rotate (0, -180, 0);
				currentDirection = "left";
			}
		}	
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		//Debug.Log ("col@"+Time.time);
		if (coll.gameObject.name == "Ground") {
			isGrounded = true;
			changeState (CharStateBhv.ST_IDLE);	
		}	
	}
}
