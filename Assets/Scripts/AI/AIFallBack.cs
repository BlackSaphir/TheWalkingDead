using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AIFallBack : StateMachineBehaviour
{
    AIAgent AIObject;
    GameObject Player;
    FirstPersonController FirstPersonController;
    PlayerManager PlayerManager;
    float distance;
    float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = animator.gameObject.GetComponent<AIAgent>().Player.gameObject;
        AIObject = animator.GetComponent<AIAgent>();
        FirstPersonController = Player.GetComponent<FirstPersonController>();
        PlayerManager = Player.GetComponent<PlayerManager>();
        timer = 0;
        animator.SetBool("targetInSight", false);
      
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = AIObject.distance;
        timer += Time.deltaTime;
        if (PlayerManager.AttackingZombies.Count == 0)
        {
            FirstPersonController.QuickEventText.SetActive(true);
            FirstPersonController.Text.text = "Run Bitch Run!!!";
        }
        if (timer>=5f)
        {
            if (distance < 20.0f)
            {
                animator.SetBool("targetInSight", true);
            }
            else
                animator.SetBool("exit",true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("PlayerFreed", false);

        if (PlayerManager.AttackingZombies.Count == 0)
        {
            FirstPersonController.QuickEventText.SetActive(false);
        }
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
