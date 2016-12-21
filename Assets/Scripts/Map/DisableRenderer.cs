using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRenderer : MonoBehaviour
{
    Renderer renderer;


    void Start()
    {
        StartCoroutine(DisableMeshRenderer());
        renderer = GetComponent<Renderer>();
        
    }


    IEnumerator DisableMeshRenderer()
    {
        this.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(4);
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
