using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : StateMachineBehaviour
{
    AIAgent AIObject;
    GameObject Player;
    float distance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = animator.gameObject.GetComponent<AIAgent>().Player;
        AIObject = animator.GetComponent<AIAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = AIObject.distance;
        if (distance > 3f)
        {
            animator.SetBool("targetInRange", false);
        }
        var targetrotation = Quaternion.LookRotation(Player.transform.position - animator.gameObject.transform.position, Vector3.up);
        animator.gameObject.transform.rotation = Quaternion.Slerp(animator.gameObject.transform.rotation, targetrotation, Time.deltaTime * 2f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
