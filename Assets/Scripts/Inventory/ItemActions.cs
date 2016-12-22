using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemActions : MonoBehaviour
{
    public GameObject PressEcollect;
    public GameObject PressEusePetrol;
    public GameObject PressErepairMobileradio;
    public GameObject PressErepairRadiotower;
    public GameObject PressEuseLightsource;
    public GameObject PressEuseKey;
    public GameObject PressEuseMobileradio;
    public Image Oil_Tank_Progressbar;
    public GameObject InventoryFull;
    FirstPersonController Player;
    InventoryManager Manager;
    public int Index;
    public int Amount = 0;

    IEnumerator InventoryFullMessage()
    {
        InventoryFull.SetActive(true);
        yield return new WaitForSeconds(3);
        InventoryFull.SetActive(false);
    }


    public IEnumerator Progressbar()
    {
        yield return new WaitForSeconds(1);
        Amount++;
        if (Amount < 180)
        {
            Oil_Tank_Progressbar.fillAmount -= 1.0f / 180.0f;
            StartCoroutine(Progressbar());
        }
        else
        {
            StopAllCoroutines();
            Manager.EnoughPetrol = false;
        }
    }

    public IEnumerator Winning()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Winning");
        //End Scene/Game
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
            else
            //Item Battery
            if (Player.Item.tag == ("item_battery"))
            {
                PressEcollect.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (Index < 5)
                    {
                        Destroy(Player.Item);
                        Manager.PickUp = ItemTypes.battery;

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
            // Item Cable
            if (Player.Item.tag == ("item_cable"))
            {
                PressEcollect.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (Index < 5)
                    {
                        Destroy(Player.Item);
                        Manager.PickUp = ItemTypes.cable;

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
            //Item Lightsource
            if (Player.Item.tag == ("item_lightsource"))
            {
                PressEcollect.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (Index < 5)
                    {
                        Destroy(Player.Item);
                        Manager.PickUp = ItemTypes.lightsources;

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
            // Item Repairset
            if (Player.Item.tag == ("item_repairset"))
            {
                PressEcollect.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (Index < 5)
                    {
                        Destroy(Player.Item);
                        Manager.PickUp = ItemTypes.repairset;

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
            // Item Key
            if (Player.Item.tag == ("item_key"))
            {
                PressEcollect.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (Index < 5)
                    {
                        Destroy(Player.Item);
                        Manager.PickUp = ItemTypes.key;

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
