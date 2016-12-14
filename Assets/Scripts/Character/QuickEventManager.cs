using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickEventManager : MonoBehaviour
{
    public float ForwardMovement;
    public float SideMovement;
    public bool CanMove;
    public int QuickEventCounter;
    public bool ItsTimeForQuicky;
    public float DeathTimer;
    public bool StartDeathTimer;
    public int CountToFree;
    public Object[] EnemyInsideTrigger;

    // Use this for initialization
    void Start()
    {
        CanMove = true;
        DeathTimer = 0;
        StartDeathTimer = false;
        CountToFree = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            ForwardMovement = Input.GetAxis("Vertical") * Time.deltaTime * 20.0f;
            SideMovement = Input.GetAxis("Horizontal") * Time.deltaTime * 20.0f;
            transform.Translate(0, 0, ForwardMovement);
            transform.Translate(SideMovement, 0, 0);
        }
        if (ItsTimeForQuicky)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    QuickEventCounter++;
                }
            }
        }
        if (StartDeathTimer)
        {
            DeathTimer += Time.deltaTime;
        }
        if (DeathTimer > 5 && QuickEventCounter <= CountToFree)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            StartDeathTimer = true;
            QuickEventCounter = 0;
            Debug.Log("Press Q + E");
            CanMove = false;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        ItsTimeForQuicky = true;
        if (QuickEventCounter >= CountToFree)
        {
            CanMove = true;
            DeathTimer = 0;
            StartDeathTimer = false;
        }
    }
}
