using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickEventManager : MonoBehaviour
{
    public float ForwardMovement;
    public float SideMovement;
    public bool CanMove;
    public bool QuickEventDone;
    public bool ItsTimeForQuicky;
    public float DeathTimer;
    public bool StartDeathTimer;   
    public KeyCode[] QuickEventButtons;    
    int Index;   

    // Use this for initialization
    void Start()
    {
        CanMove = true;
        DeathTimer = 0;
        StartDeathTimer = false;        
        QuickEventDone = false;
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
            if (Input.anyKeyDown)
            {
                KeyCode Temp = QuickEventButtons[Index];
                if (Input.GetKeyDown(Temp))
                {
                    Debug.Log("Button is pressed");
                    Index++;
                    if (Index == QuickEventButtons.Length)
                    {
                        QuickEventDone = true;
                    }
                }
                else
                    Destroy(this.gameObject);
            }
        }
        if (StartDeathTimer)
        {
            DeathTimer += Time.deltaTime;
        }
        if (DeathTimer > 5 && !QuickEventDone)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ItsTimeForQuicky = true;
            StartDeathTimer = true;
            QuickEventDone = false;
            CanMove = false;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (QuickEventDone)
        {
            CanMove = true;
            DeathTimer = 0;
            StartDeathTimer = false;
        }
    }
}
