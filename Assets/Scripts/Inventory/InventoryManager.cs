using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes { mobileradio, petrol, lightsources, key, cable, repairset, battery };

public class InventoryManager : MonoBehaviour
{
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    public GameObject Slot5;

    public List<ItemTypes> InventoryList;

    //public ItemTypes MyItem;

    public int PanelIndex;


    public void UpdateList()
    {
        for (int i = 0; i < InventoryList.Count; i++)
        {
            switch (InventoryList[i])
            {
                case ItemTypes.mobileradio:
                    GameObject temp = Instantiate(Resources.Load<GameObject>("mobileradio"));
                    temp.transform.SetParent(Slot3.transform, false);
                   
                    break;
                case ItemTypes.petrol:
                    break;
                case ItemTypes.lightsources:
                    break;
                case ItemTypes.key:
                    break;
                case ItemTypes.cable:
                    break;
                case ItemTypes.repairset:
                    break;
                case ItemTypes.battery:
                    break;
                default:
                    break;
            }
        }
    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
