using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRenderer : MonoBehaviour
{
    Renderer renderer;


    void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }


   void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            renderer.enabled = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            renderer.enabled = false;
        }
    }

}
