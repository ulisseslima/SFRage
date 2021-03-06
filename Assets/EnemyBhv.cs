﻿using UnityEngine;
using System.Collections;

public class EnemyBhv : MonoBehaviour {

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
	private bool onGround; // is player on the ground?
	private AnimatorStateInfo animatorState;

	KeyCode currkey = KeyCode.RightArrow;
	int currentState = CharStateBhv.ST_WALKING_F;
	int currentAtkState = CharStateBhv.ST_ATK_NONE;
	int currentStateMod = CharStateBhv.ST_DISTANCE_FAR;
	string currentDirection = "right";
	Animator animator;
	public float walkSpeed = 1; // player left right walk speed
	public int jumpForce = 150;
	
	// Use this for initialization
	void Start ()
	{
		player = transform.parent;
		rb = GetComponent<Rigidbody2D> ();
		animator = transform.GetComponent<Animator> ();
		
		setStateMod (CharStateBhv.ST_DISTANCE_FAR);

		player.Rotate (0, -180, 0);
	}
	
	void FixedUpdate ()
	{
		if ((isGrounded() && !isCrouching()) && currkey == up) {
			setState (CharStateBhv.ST_JUMPING);
			//Debug.Log ("jumping");
			rb.AddForce (new Vector2 (0, jumpForce));
			onGround = false;
			//Debug.Log("registering input @"+Time.time);
		} else if (!isCrouching() && (currkey == right || currkey == left)) { 
			if (currkey == right) {
				if (isGrounded() && !isCrouching())
					setState (CharStateBhv.ST_WALKING_F);
				
				if (!isCrouching()) {
					translate (Vector3.right);
					//changeDirection ("right");
				}
			} else if (currkey == left) {
				if (isGrounded() && !isCrouching())
					setState (CharStateBhv.ST_WALKING_B);
				
				if (!isCrouching()) {
					translate (Vector3.left);
					//changeDirection ("left");
				}
			}
		} else if (currkey == down) { 
			setState (CharStateBhv.ST_CROUCHING);
			crouching = true;
		} else {
			setState (CharStateBhv.ST_STANDING);
			crouching = false;
			//checkAttackState();
			if (currkey == lp) {
				setAtkState (CharStateBhv.ST_ATK_LP);
			} else if (currkey ==  (mp)) {
				setAtkState (CharStateBhv.ST_ATK_MP);
			} else if (currkey ==  (hp)) {
				setAtkState (CharStateBhv.ST_ATK_HP);
			} else if (currkey == (lk)) {
				setAtkState (CharStateBhv.ST_ATK_LK);
			} else if (currkey ==  (mk)) {
				setAtkState (CharStateBhv.ST_ATK_MK);
			} else if (currkey ==  (hk)) {
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
	
	private bool isGrounded ()
	{
		return onGround == true && getState () != 5;
	}
	
	private bool isCrouching ()
	{
		return getState () == 6 || 
			crouching || 
				currAnimStateEq(CharStateBhv.ANIM_ST_CROUCHING);
	}
	
	private int getState ()
	{
		return animator.GetInteger ("state");
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
			onGround = true;
			crouching = false;
			setState (CharStateBhv.ST_STANDING);	
		}	
	}
	
	/**
	 * @param name - animation state name.
	 * @returns true if the current animation state equals the one passed in.
	 */
	private bool currAnimStateEq(int hash) {
		animatorState = animator.GetCurrentAnimatorStateInfo (0);
		return animatorState.shortNameHash == hash;
	}
}
