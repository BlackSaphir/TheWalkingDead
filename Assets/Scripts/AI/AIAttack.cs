using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AIAttack : StateMachineBehaviour
{
    AIAgent AIObject;
    GameObject Player;
    TriggerManager TriggerManager;
    FirstPersonController FirstPersonController;
    float distance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = animator.gameObject.GetComponent<AIAgent>().Player.gameObject;
        AIObject = animator.GetComponent<AIAgent>();
        FirstPersonController = Player.GetComponent<FirstPersonController>();
        TriggerManager = Player.gameObject.GetComponent<TriggerManager>();

        if (!TriggerManager.AttackingZombies.Contains(this))
        {
            TriggerManager.AttackingZombies.Add(this);
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = AIObject.distance;
        if (distance > 3f)
        {
            animator.SetBool("targetInRange", false);
        }
        if (Player != null)
        {
            var targetrotation = Quaternion.LookRotation(Player.transform.position - animator.gameObject.transform.position, Vector3.up);
            animator.gameObject.transform.rotation = Quaternion.Slerp(animator.gameObject.transform.rotation, targetrotation, Time.deltaTime * 2f);

            FirstPersonController.ItsTimeForQuicky = true;
            FirstPersonController.StartDeathTimer = true;
            if (FirstPersonController.Index == FirstPersonController.QuickEventButtons.Length)
            {
                FirstPersonController.QuickEventDone = true;
                FirstPersonController.ItsTimeForQuicky = false;
                animator.SetBool("PlayerFreed", true);
                //FirstPersonController.Text.text = "Run Bitch Run";                          
            }
            else
                FirstPersonController.QuickEventDone = false;

        }
        if (Player == null)
        {
            animator.SetBool("targetAlive", false);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player != null)
        {
            FirstPersonController.ItsTimeForQuicky = false;
            FirstPersonController.QuickEventText.SetActive(false);
            FirstPersonController.Index = 0;
            FirstPersonController.CanMove = true;
            FirstPersonController.QuickEventDone = true;
        }
        animator.SetBool("targetInRange", false);

        TriggerManager.AttackingZombies.Remove(this);

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
