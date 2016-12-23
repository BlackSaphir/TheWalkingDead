using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : MonoBehaviour
{
    public List<AIAttack> AttackingZombies;
    public FirstPersonController FirstPersonController;
    public Animator animator;
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {

        if (AttackingZombies.Count >= 3)
        {
            FirstPersonController.TooManyZombies = true;
        }
        if (FirstPersonController.CanMove)
        {
            //IsMoving und nicht isMoving...
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (FirstPersonController.AttackAnimation)
        {
            animator.SetBool("IsAttacking", true);
            FirstPersonController.AttackAnimation = false;
        }



    }
}
