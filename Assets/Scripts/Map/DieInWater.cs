using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInWater : MonoBehaviour
{
    void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.tag == "WaterPlane")
        {
            Destroy(this.gameObject);
        }
    }
}
