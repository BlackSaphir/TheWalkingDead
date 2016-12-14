using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public float distance;
    public Vector3 target;
    public GameObject Player;
    public float timer;
    public Vector3 offset;
    // Use this for initialization
    void Start()
    {
        timer = 8;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        distance = Vector3.Distance(this.transform.position, Player.transform.position);
        if (timer>=10)
        {
            SetTarget();
            timer = 0;
        }

    }

    public void Patrol()
    {
        offset = target - transform.position;
        //this.transform.LookAt(target);
        //this.transform.Translate(offset*Time.deltaTime);
        this.transform.LookAt(target);
        this.transform.Translate(Vector3.forward * Time.deltaTime);
    }

    public Vector3 SetTarget()
    {
        target = new Vector3(Random.Range(this.transform.position.x - 10, this.transform.position.x + 20), 0, Random.Range(this.transform.position.z - 10, this.transform.position.z + 20));
        
        return target;
    }
}
