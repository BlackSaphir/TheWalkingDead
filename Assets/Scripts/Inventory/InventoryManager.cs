using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes { mobileradio, petrol, lightsources, key, cable, repairset };

public class InventoryManager : MonoBehaviour
{
    public ItemTypes MyItem;
    public void Test(MyItem item)
    {
         switch(item.Item)
        {
            case ItemTypes.cable:
            break;
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

public class MyItem
{
    public ItemTypes Item = ItemTypes.cable;
}
