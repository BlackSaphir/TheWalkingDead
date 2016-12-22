using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AIMoveToTarget : StateMachineBehaviour
{
    float distance;
    AIAgent AIObject;
    GameObject Player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = animator.gameObject.GetComponent<AIAgent>().Player.gameObject;
        AIObject = animator.GetComponent<AIAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 fwd = AIObject.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(AIObject.transform.position, fwd, out hit, 1.0f))
        {
            if (hit.collider.tag == "Player")
            {
                animator.SetBool("targetInRange", true);
                Player.GetComponent<FirstPersonController>().CanMove = false;
            }

        }

        distance = AIObject.distance;
        Debug.Log(distance);
        if (distance > 20.0f)
        {
            animator.SetBool("targetInSight", false);
        }
        if (Player != null)
        {
            var targetrotation = Quaternion.LookRotation(Player.transform.position - animator.gameObject.transform.position, Vector3.up);
            animator.gameObject.transform.rotation = Quaternion.Slerp(animator.gameObject.transform.rotation, targetrotation, Time.deltaTime * 2f);
            animator.gameObject.transform.Translate(Vector3.forward * Time.deltaTime);
        }
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
