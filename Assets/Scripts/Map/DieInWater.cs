using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieInWater : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Pipi");
        if (collision.gameObject.tag == "WaterPlane")
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("LooseScene");
        }
    }
}
