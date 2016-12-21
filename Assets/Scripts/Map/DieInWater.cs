using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieInWater : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Sharks");
        if (collision.gameObject.tag == "WaterPlane")
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("LooseScene");
        }
    }
}
