using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TriggerManager : MonoBehaviour
{
    public List<AIAttack> AttackingZombies;
    public FirstPersonController FirstPersonController;

    // Update is called once per frame
    void Update()
    {
        if (AttackingZombies.Count >= 3)
        {
            FirstPersonController.TooManyZombies = true;
        }
    }
}
