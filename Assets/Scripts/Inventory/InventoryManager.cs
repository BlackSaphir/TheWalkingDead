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
    public int index;
    private GameObject[] array;
    private GameObject RecTemp;





    public List<ItemTypes> InventoryList;

    public int PanelIndex;


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
                        array[i] = temp;
                        temp.transform.SetParent(ParentPanel.transform, false);
                        break;
                    case ItemTypes.petrol:
                        GameObject temp1 = Instantiate(Resources.Load<GameObject>("petrol_texture"));
                        temp1.transform.position = Initial + Vector3.down * Delta * i;
                        array[i] = temp1;
                        temp1.transform.SetParent(ParentPanel.transform, false);
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

        if (array[inventorySlot] != null)
        {
            RecTemp.transform.SetParent(array[inventorySlot].transform, false);
        }
    }

    public void SelectItem()
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Moveselector(i);
            }
        }
    }

    public void DropItem(int index)
    {
        if (array[this.index] != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                int next = (this.index + 1) % array.Length;

                if (array[this.index] != null)
                {
                    ItemActions.Index--;
                    string ItemName = InventoryList[this.index].ToString();
                    InventoryList[this.index] = ItemTypes.empty;
                    Instantiate(Resources.Load<GameObject>(ItemName + "_object"), new Vector3(Player.transform.position.x, Player.transform.position.y - 1, Player.transform.position.z), Quaternion.identity);
                    Destroy(array[this.index]);
                    array[this.index] = null;
                    Moveselector(next);
                }
            }
        }
    }

    public void UseItem(int index)
    {
        if (array[this.index] != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                int next = (this.index + 1) % array.Length;

                if (array[this.index] != null)
                {
                    ItemActions.Index--;
                    string ItemName = InventoryList[this.index].ToString();
                    InventoryList[this.index] = ItemTypes.empty;
                    Instantiate(Resources.Load<GameObject>(ItemName + "_object"), new Vector3(Player.transform.position.x, Player.transform.position.y - 1, Player.transform.position.z), Quaternion.identity);
                    Destroy(array[this.index]);
                    array[this.index] = null;
                    Moveselector(next);
                }
            }
        }
    }

    void Moveselector(int tslot)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int currentIndex = (tslot + i) % array.Length;
            if (array[currentIndex] != null)
            {
                SetRectPosition(currentIndex);
                index = currentIndex;
                RecTemp.GetComponent<Image>().enabled = true;
                return;
            }
        }
        index = 0;
    }

    // Use this for initialization
    void Start()
    {
        ItemActions = GetComponent<ItemActions>();
        array = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {
        SelectItem();
        DropItem(index);
    }
}
