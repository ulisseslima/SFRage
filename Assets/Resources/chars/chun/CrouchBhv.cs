using UnityEngine;
using System.Collections;

public class CrouchBhv : StateMachineBehaviour {
	private bool reversing;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		float norm = stateInfo.normalizedTime;

		animator.speed = 1f;
		//Debug.Log ("enter: " + animator.speed + "@" + norm);
		reversing = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		float norm = stateInfo.normalizedTime;
		
		if (norm >= .55f) {
			animator.speed = -.2f;
			reversing = true;
			//Debug.Log ("speed: " + animator.speed + ", " + elapsed + "@" + norm);
		} else if (reversing && norm > 0 && norm <= .4) {
			animator.speed = .3f;
			reversing = false;
		}
		
		//Debug.Log ("speed: " + animator.speed + "@" + norm);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		float norm = stateInfo.normalizedTime;

		animator.speed = 1f;
		//Debug.Log ("exit: " + animator.speed + "@" + norm);
		reversing = false;
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
