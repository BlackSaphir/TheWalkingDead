using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInWater : MonoBehaviour
{
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "WaterPlane")
        {
            Destroy(this.gameObject);
        }
    }
}
