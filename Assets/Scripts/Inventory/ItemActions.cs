using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemActions : MonoBehaviour
{
    public GameObject PressE;
    public GameObject InventoryFull;
    FirstPersonController Player;
    InventoryManager Manager;
    public int Index;

    IEnumerator InventoryFullMessage()
    {
        InventoryFull.SetActive(true);
        yield return new WaitForSeconds(3);
        InventoryFull.SetActive(false);
    }

    public void CollectItem()
    {
        if (Player.IsColliding == true)
        {
            if (Player.Item.tag == ("item_mobileradio"))
            {
                PressE.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (Index < 5)
                    {
                        Destroy(Player.Item);
                        Manager.PickUp = ItemTypes.mobileradio;

                        Manager.UpdateList();
                        Index++;
                        Player.IsColliding = false;
                    }
                    else
                    {
                        StartCoroutine(InventoryFullMessage());
                    }
                    PressE.SetActive(false);
                }
            }
        }

        else
        {
            PressE.SetActive(false);

        }

    }


    public void DropItem()
    {

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
