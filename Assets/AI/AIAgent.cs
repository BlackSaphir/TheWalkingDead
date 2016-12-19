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
    private float rotationtime;
    private Vector3 velocity;
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
        if (timer >= 10)
        {
            SetTarget();
            timer = 0;
        }

    }

    public void Patrol()
    {
        velocity = Vector3.zero;
        rotationtime = 0;
        offset = target - transform.position;
        var targetrotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, Time.deltaTime * 2f);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.3f, 3f);
    }

    public Vector3 SetTarget()
    {
        target = new Vector3(Random.Range(this.transform.position.x - 10, this.transform.position.x + 10), this.transform.position.y, Random.Range(this.transform.position.z - 10, this.transform.position.z + 10));

        return target;
    }
}
