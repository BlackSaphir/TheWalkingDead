using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemActions : MonoBehaviour
{
    public GameObject PressEcollect;
    public GameObject PressEuse;
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
            //Empty Barrel
            if (Player.Item.tag == ("barrel_empty"))
            {
                PressEcollect.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(Player.Item);
                    Player.IsColliding = false;
                }
            }
            else
            //Item MobileRadio
            if (Player.Item.tag == ("item_mobileradio"))
            {
                PressEcollect.SetActive(true);
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
                    PressEcollect.SetActive(false);
                }
            }
            else
            //Item Petrol
            if (Player.Item.tag == ("item_petrol"))
            {
                PressEcollect.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (Index < 5)
                    {
                        Destroy(Player.Item);
                        Manager.PickUp = ItemTypes.petrol;

                        Manager.UpdateList();
                        Index++;
                        Player.IsColliding = false;
                    }
                    else
                    {
                        StartCoroutine(InventoryFullMessage());
                    }
                    PressEcollect.SetActive(false);
                }
            }
        }

        else
        {
            PressEcollect.SetActive(false);

        }

    }


    public void UseItem()
    {
        if (Player.IsColliding_OilTrigger == true)
        {
            PressEuse.SetActive(true);
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
