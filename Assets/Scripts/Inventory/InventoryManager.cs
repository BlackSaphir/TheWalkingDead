using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public enum ItemTypes { empty, mobileradio, petrol, lightsources, key, cable, repairset, battery };

public class InventoryManager : MonoBehaviour
{
    public ItemActions ItemActions;
    public FirstPersonController Player;
    public ItemTypes PickUp;
    public GameObject ParentPanel;
    public Vector3 Initial;
    public int Delta = 45;
    public GameObject Rectangle;
    public GameObject Containerdoor;
    public int index;
    public GameObject[] Inventory;
    private GameObject RecTemp;
    public List<ItemTypes> InventoryList;
    public int PanelIndex;
    public bool RepairedRadiotower;
    public bool RepairedMobileradio;
    public bool EnoughPetrol;
    public bool CallForRescue;

    public GameObject[] Torches;

    public void UpdateList()
    {
        for (int i = 0; i < InventoryList.Count; i++)
        {
            if (InventoryList[i] == ItemTypes.empty)
            {
                InventoryList[i] = PickUp;

                switch (InventoryList[i])
                {
                    case ItemTypes.mobileradio:
                        GameObject temp = Instantiate(Resources.Load<GameObject>("mobileradio_texture"));
                        temp.transform.position = Initial + Vector3.down * Delta * i;
                        Inventory[i] = temp;
                        temp.transform.SetParent(ParentPanel.transform, false);
                        break;
                    case ItemTypes.petrol:
                        GameObject temp1 = Instantiate(Resources.Load<GameObject>("petrol_texture"));
                        temp1.transform.position = Initial + Vector3.down * Delta * i;
                        Inventory[i] = temp1;
                        temp1.transform.SetParent(ParentPanel.transform, false);
                        break;
                    case ItemTypes.lightsources:
                        GameObject temp2 = Instantiate(Resources.Load<GameObject>("lightsources_texture"));
                        temp2.transform.position = Initial + Vector3.down * Delta * i;
                        Inventory[i] = temp2;
                        temp2.transform.SetParent(ParentPanel.transform, false);
                        break;
                    case ItemTypes.key:
                        GameObject temp3 = Instantiate(Resources.Load<GameObject>("key_texture"));
                        temp3.transform.position = Initial + Vector3.down * Delta * i;
                        Inventory[i] = temp3;
                        temp3.transform.SetParent(ParentPanel.transform, false);
                        break;
                    case ItemTypes.cable:
                        GameObject temp4 = Instantiate(Resources.Load<GameObject>("cable_texture"));
                        temp4.transform.position = Initial + Vector3.down * Delta * i;
                        Inventory[i] = temp4;
                        temp4.transform.SetParent(ParentPanel.transform, false);
                        break;
                    case ItemTypes.repairset:
                        GameObject temp5 = Instantiate(Resources.Load<GameObject>("repairset_texture"));
                        temp5.transform.position = Initial + Vector3.down * Delta * i;
                        Inventory[i] = temp5;
                        temp5.transform.SetParent(ParentPanel.transform, false);
                        break;
                    case ItemTypes.battery:
                        GameObject temp6 = Instantiate(Resources.Load<GameObject>("battery_texture"));
                        temp6.transform.position = Initial + Vector3.down * Delta * i;
                        Inventory[i] = temp6;
                        temp6.transform.SetParent(ParentPanel.transform, false);
                        break;
                    default:
                        break;
                }
                break;
            }
        }
        SetRectPosition(0);
    }

    public void SetRectPosition(int inventorySlot)
    {
        if (RecTemp == null)
        {
            RecTemp = Instantiate(Rectangle);
        }

        if (Inventory[inventorySlot] != null)
        {
            RecTemp.transform.SetParent(Inventory[inventorySlot].transform, false);
        }
    }

    public void SelectItem()
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Moveselector(i);
            }
        }
    }

    public void DropItem(int index)
    {
        if (Inventory[this.index] != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                int next = (this.index + 1) % Inventory.Length;

                if (Inventory[this.index] != null)
                {
                    ItemActions.Index--;
                    string ItemName = InventoryList[this.index].ToString();
                    InventoryList[this.index] = ItemTypes.empty;
                    Instantiate(Resources.Load<GameObject>(ItemName + "_object"), new Vector3(Player.transform.position.x, Player.transform.position.y - 1, Player.transform.position.z), Quaternion.identity);
                    Destroy(Inventory[this.index]);
                    Inventory[this.index] = null;
                    Moveselector(next);
                }
            }
        }
    }

    void Moveselector(int tslot)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            int currentIndex = (tslot + i) % Inventory.Length;
            if (Inventory[currentIndex] != null)
            {
                SetRectPosition(currentIndex);
                index = currentIndex;
                RecTemp.GetComponent<Image>().enabled = true;
                return;
            }
        }
        index = 0;
    }

    public void UseItem(int index)

    {
        //Use Petrol
        if (Player.IsColliding_OilTrigger == true)
        {
            if (Inventory[this.index] != null)
            {
                int next = (this.index + 1) % Inventory.Length;
                if (Inventory[this.index] != null)
                {
                    if (InventoryList[this.index] == ItemTypes.petrol)
                    {
                        ItemActions.PressEusePetrol.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            // Was soll passieren hier hin
                            EnoughPetrol = true;
                            ItemActions.Oil_Tank_Progressbar.GetComponent<Image>().enabled = true;
                            ItemActions.StartCoroutine(ItemActions.Progressbar());
                            //
                            ItemActions.Index--;
                            InventoryList[this.index] = ItemTypes.empty;
                            Destroy(Inventory[this.index]);
                            Inventory[this.index] = null;
                            Moveselector(next);
                            ItemActions.PressEusePetrol.SetActive(false);
                        }
                    }
                }
            }
        }
        else
            ItemActions.PressEusePetrol.SetActive(false);
        //Use Battery on Mobileradio;
        if (Player.IsColliding_BaseTrigger && InventoryList.Contains(ItemTypes.battery) && InventoryList.Contains(ItemTypes.mobileradio))
        {
            if (Inventory[this.index] != null)
            {
                int next = (this.index + 1) % Inventory.Length;
                if (Inventory[this.index] != null)
                {
                    ItemActions.PressErepairMobileradio.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //Was soll passieren hier rein
                        RepairedMobileradio = true;
                        ItemActions.SoundRepairMobileradio.GetComponent<AudioSource>().Play();
                        //
                        ItemActions.Index--;
                        int indexBaterie = InventoryList.FindIndex(a => a == ItemTypes.battery);
                        InventoryList[indexBaterie] = ItemTypes.empty;
                        Destroy(Inventory[indexBaterie]);
                        Inventory[indexBaterie] = null;
                        Moveselector(next);
                        ItemActions.PressErepairMobileradio.SetActive(false);
                    }
                }
            }

        }
        else
            ItemActions.PressErepairMobileradio.SetActive(false);
        //Use Cable and Repairset on Radiotower
        if (Player.IsColliding_RadioTrigger && InventoryList.Contains(ItemTypes.cable) && InventoryList.Contains(ItemTypes.repairset))
        {
            if (Inventory[this.index] != null)
            {
                int next = (this.index + 1) % Inventory.Length;
                if (Inventory[this.index] != null)
                {
                    ItemActions.PressErepairRadiotower.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        
                        //Was soll passieren hier rein
                        RepairedRadiotower = true;
                        //
                        ItemActions.Index -= 2;
                        int indexCable = InventoryList.FindIndex(a => a == ItemTypes.cable);
                        InventoryList[indexCable] = ItemTypes.empty;
                        Destroy(Inventory[indexCable]);
                        Inventory[indexCable] = null;
                        int indexRepairset = InventoryList.FindIndex(a => a == ItemTypes.repairset);
                        InventoryList[indexRepairset] = ItemTypes.empty;
                        Destroy(Inventory[indexRepairset]);
                        Inventory[indexRepairset] = null;
                        Moveselector(next);
                        ItemActions.PressErepairRadiotower.SetActive(false);
                    }
                }
            }
        }
        else
            ItemActions.PressErepairRadiotower.SetActive(false);
        //Use Key
        if (Player.IsColliding_Base_key_trigger == true)
        {
            if (Inventory[this.index] != null)
            {
                int next = (this.index + 1) % Inventory.Length;
                if (Inventory[this.index] != null)
                {
                    if (InventoryList[this.index] == ItemTypes.key)
                    {
                        ItemActions.PressEuseKey.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            // Was soll passieren hier hin
                            Containerdoor.transform.Rotate(180, 90, 180);
                            ItemActions.SoundKey.GetComponent<AudioSource>().Play();

                            ItemActions.Index--;
                            InventoryList[this.index] = ItemTypes.empty;
                            Destroy(Inventory[this.index]);
                            Inventory[this.index] = null;
                            Moveselector(next);
                            ItemActions.PressEuseKey.SetActive(false);
                        }
                    }
                }
            }
        }
        else
            ItemActions.PressEuseKey.SetActive(false);
        //Use repaired Mobileradio
        if (Player.IsColliding_RadioTrigger && InventoryList.Contains(ItemTypes.mobileradio) && RepairedMobileradio && RepairedRadiotower && EnoughPetrol)
        {
            if (Inventory[this.index] != null)
            {
                int next = (this.index + 1) % Inventory.Length;
                if (Inventory[this.index] != null)
                {
                    ItemActions.PressEuseMobileradio.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //Was soll passieren hier rein
                        
                        CallForRescue = true;
                        ItemActions.SoundUseMobileradio.GetComponent<AudioSource>().Play(3);
                        //
                        Moveselector(next);
                        ItemActions.PressEuseMobileradio.SetActive(false);
                    }
                }
            }
        }
        else
            ItemActions.PressEuseMobileradio.SetActive(false);
        //Use Lightsource
        if (Player.IsColliding_rescuePlattform_Trigger && RepairedMobileradio && RepairedRadiotower && EnoughPetrol && CallForRescue)
        {
            if (Inventory[this.index] != null)
            {
                int next = (this.index + 1) % Inventory.Length;
                if (Inventory[this.index] != null)
                {
                    if (InventoryList[this.index] == ItemTypes.lightsources)
                    {

                        ItemActions.PressEuseLightsource.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            // Was soll passieren hier hin
                            ItemActions.StartCoroutine(ItemActions.Winning());
                            Torches = new GameObject[4];
                            for (int i = 0; i < Torches.Length; i++)
                            {
                                Torches[i] = GameObject.Find("Torch"+i);
                                Torches[i].GetComponent<Torchelight>().IntensityLight = 3;
                            }
                            //
                            ItemActions.Index--;
                            InventoryList[this.index] = ItemTypes.empty;
                            Destroy(Inventory[this.index]);
                            Inventory[this.index] = null;
                            Moveselector(next);
                            ItemActions.PressEuseLightsource.SetActive(false);
                        }
                    }
                }
            }
        }

    }

    void Start()
    {
        RepairedRadiotower = false;
        RepairedMobileradio = false;
        EnoughPetrol = false;
        ItemActions = GetComponent<ItemActions>();
        Inventory = new GameObject[5];
    }

    void Update()
    {
        SelectItem();
        DropItem(index);
        UseItem(index);       
    }
}
