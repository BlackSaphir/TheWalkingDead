using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickEventManager : MonoBehaviour
{
    public GameObject QuickEventText;
    public float ForwardMovement;
    public float SideMovement;
    public bool CanMove;
    public bool QuickEventDone;
    public bool ItsTimeForQuicky;
    public float DeathTimer;
    public bool StartDeathTimer;
    public KeyCode[] QuickEventButtons;
    public bool RightButtonPressed;
    public Text Text;
    int Index;

    // Use this for initialization
    void Start()
    {
        CanMove = true;
        DeathTimer = 0;
        StartDeathTimer = false;
        QuickEventDone = false;
        RightButtonPressed = false;
        QuickEventText.SetActive(false);
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
            if (Index < QuickEventButtons.Length)
            {

                QuickEventText.SetActive(true);
                Text.text = "Press " + QuickEventButtons[Index].ToString();
                if (Input.anyKeyDown)
                {
                    KeyCode Temp = QuickEventButtons[Index];
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                    {
                        //Ignore W,A,S,D bei Quick Event                    
                    }
                    else if (Input.GetKeyDown(Temp))
                    {
                        RightButtonPressed = true;                        
                        Index++;
                        if (Index == QuickEventButtons.Length)
                        {
                            Text.text = "Run Bitch Run";
                            QuickEventDone = true;                                                      
                        }
                    }
                    else
                        Destroy(this.gameObject);
                }
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
    public void OnTriggerExit(Collider other)
    {
        ItsTimeForQuicky = false;
        QuickEventText.SetActive(false);
        Index = 0;
    }
}
