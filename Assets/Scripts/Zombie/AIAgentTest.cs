using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgentTest : MonoBehaviour
{
    public float distance;
    public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(this.transform.position, Player.transform.position);	
	}
}
