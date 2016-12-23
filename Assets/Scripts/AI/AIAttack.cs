using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AIAttack : StateMachineBehaviour
{
    AIAgent AIObject;
    GameObject Player;
    PlayerManager PlayerManager;
    FirstPersonController FirstPersonController;
    public int Index;    
    float distance;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = animator.gameObject.GetComponent<AIAgent>().Player.gameObject;
        AIObject = animator.GetComponent<AIAgent>();
        FirstPersonController = Player.GetComponent<FirstPersonController>();
        PlayerManager = Player.gameObject.GetComponent<PlayerManager>();

        if (!PlayerManager.AttackingZombies.Contains(this))
        {
            PlayerManager.AttackingZombies.Add(this);
        }
        FirstPersonController.AttackAnimation = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Index = FindIndexOfThis();
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


            if (FirstPersonController.Index == FirstPersonController.QuickEventButtons.Length && Index == 0)
            {                
                FirstPersonController.AttackAnimation = true;
                FirstPersonController.QuickEventDone = true;
                FirstPersonController.ItsTimeForQuicky = false;
                animator.SetBool("PlayerFreed", true);
            }
            else
            {
                FirstPersonController.QuickEventDone = false;                
            }

        }
        if (Player == null)
        {
            animator.SetBool("targetAlive", false);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player != null && Index == 0)
        {
            FirstPersonController.ItsTimeForQuicky = false;
            FirstPersonController.QuickEventText.SetActive(false);
            FirstPersonController.Index = 0;
            FirstPersonController.CanMove = true;
            FirstPersonController.QuickEventDone = true;
        }
        animator.SetBool("targetInRange", false);

        PlayerManager.AttackingZombies.Remove(this);

    }

    private int FindIndexOfThis()
    {
        for (int i = 0; i < PlayerManager.AttackingZombies.Count; i++)
        {
            if (PlayerManager.AttackingZombies[i].Equals(this))
            {
                return i;
            }
        }
        return -1;
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
