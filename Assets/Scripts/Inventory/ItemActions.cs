using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemActions : MonoBehaviour
{
    public GameObject PressE;
    FirstPersonController Player;
    InventoryManager Manager;

    public void CollectItem()
    {
        if (Player.IsColliding == true)
        {

            PressE.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                Player.Item.SetActive(false);
                PressE.SetActive(false);
               
            }
        }
        else
        {
            PressE.SetActive(false);
          
        }
    }

    // Use this for initialization
    void Start()
    {
        Manager = GetComponent<InventoryManager>();
        Player = GetComponent<FirstPersonController>();

    }

    // Update is called once per frame
    void Update()
    {
        CollectItem();
    }
}
