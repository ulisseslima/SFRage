using UnityEngine;
using System.Collections;

public class PlayerBhv : MonoBehaviour
{
	private Transform player;
	private Rigidbody2D rb;
	private KeyCode up = KeyCode.UpArrow;
	private KeyCode left = KeyCode.LeftArrow;
	private KeyCode down = KeyCode.DownArrow;
	private KeyCode right = KeyCode.RightArrow;
	private KeyCode lp = KeyCode.A;
	private KeyCode mp = KeyCode.S;
	private KeyCode hp = KeyCode.D;
	private KeyCode lk = KeyCode.Z;
	private KeyCode mk = KeyCode.X;
	private KeyCode hk = KeyCode.C;
	private bool jumping;
	private bool walking;
	private bool backingUp;
	private bool crouching;
	private bool isGrounded; // is player on the ground?

	int currentState = CharStateBhv.ST_JUMPING;
	int currentAtkState = CharStateBhv.ST_ATK_NONE;
	int currentStateMod = CharStateBhv.ST_DISTANCE_FAR;
	string currentDirection = "left";
	public Animator animator;
	public float walkSpeed = 1; // player left right walk speed
	public int jumpForce = 150;

	// Use this for initialization
	void Start ()
	{
		player = transform.parent;
		rb = GetComponent<Rigidbody2D> ();
		animator = transform.GetComponent<Animator> ();

		setStateMod (CharStateBhv.ST_DISTANCE_FAR);
	}
	
	void FixedUpdate ()
	{
		if (isGrounded && Input.GetKeyDown (up)) {
			setState (CharStateBhv.ST_JUMPING);
			rb.AddForce (new Vector2 (0, jumpForce));
			isGrounded = false;
			//Debug.Log("registering input @"+Time.time);
		} else if (Input.GetKey (right) || Input.GetKey (left)) { 
			if (Input.GetKey (right)) {
				setState (CharStateBhv.ST_WALKING_F);
				translate (Vector3.right);
			} else if (Input.GetKey (left)) {
				setState (CharStateBhv.ST_WALKING_B);
				translate (Vector3.left);
			}
		} else if (Input.GetKey (down)) { 
			setState (CharStateBhv.ST_CROUCHING);
		} else {
			setState (CharStateBhv.ST_STANDING);
			//checkAttackState();
			if (Input.GetKey (lp)) {
				setAtkState (CharStateBhv.ST_ATK_LP);
			} else if (Input.GetKey (mp)) {
				setAtkState (CharStateBhv.ST_ATK_MP);
			} else if (Input.GetKey (hp)) {
				setAtkState (CharStateBhv.ST_ATK_HP);
			} else if (Input.GetKey (lk)) {
				setAtkState (CharStateBhv.ST_ATK_LK);
			} else if (Input.GetKey (mk)) {
				setAtkState (CharStateBhv.ST_ATK_MK);
			} else if (Input.GetKey (hk)) {
				setAtkState (CharStateBhv.ST_ATK_HK);
			} else {
				setAtkState (CharStateBhv.ST_ATK_NONE);
			}
		}
	}

	void translate (Vector3 direction)
	{
		player.Translate (direction * walkSpeed * Time.deltaTime);
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

	private void setState (int state)
	{
		animator.SetInteger ("state", state);
		currentState = state;
	}

	private void setAtkState (int state)
	{
		animator.SetInteger ("atk_state", state);
		currentAtkState = state;
	}

	private void setStateMod (int state)
	{
		animator.SetInteger ("state_mod", state);
		currentStateMod = state;
	}

	void changeDirection (string direction)
	{
		if (currentDirection != direction) {
			if (direction == "right") {
				player.Rotate (0, 180, 0);
				currentDirection = "right";
			} else if (direction == "left") {
				player.Rotate (0, -180, 0);
				currentDirection = "left";
			}
		}	
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		//Debug.Log ("col@"+Time.time);
		if (coll.gameObject.name == "Ground") {
			isGrounded = true;
			setState (CharStateBhv.ST_STANDING);	
		}	
	}
}
