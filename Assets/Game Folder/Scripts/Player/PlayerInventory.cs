using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventorySlot = 1;
    [SerializeField] private List<GameObject> inventory = new List<GameObject>();
    [SerializeField] private Transform inventoryStorage;
    [SerializeField] private int itemInInventory = 0;
    private void OnEnable()
    {
        Actions.AddObjectToinventory += AddObjectToInventory;
        Actions.ThrowGarbage += ThrowGarbage;
        Funcs.GetItemInInventory += GetItemInventory;
    }

    private void OnDisable()
    {
        Actions.AddObjectToinventory -= AddObjectToInventory;
        Actions.ThrowGarbage -= ThrowGarbage;
        Funcs.GetItemInInventory -= GetItemInventory;
    }

    private int[] GetItemInventory()
    {
        int[] temp = new int[2];
        temp[0] = itemInInventory;
        temp[1] = inventorySlot;
        return temp;
    }
    private void ThrowGarbage()
    {
        if (inventory.Count>0)
        {
            AudioManager.instance.PlayMusic("Pickup");
            foreach (Transform item in inventoryStorage)
            {
                Destroy(item.gameObject);
            }
            inventory = new List<GameObject>();
        }
        itemInInventory = 0;
        Actions.RefreshInventory?.Invoke();
    }

    private async void AddObjectToInventory(GameObject obj)
    {
        if (inventory.Count < inventorySlot)
        {
            AudioManager.instance.PlayMusic("Pickup");
            GetComponent<Animator>().SetTrigger("Pickup");
            obj.transform.SetParent(inventoryStorage,false);
            inventory.Add(obj);
            itemInInventory++;
            Actions.RefreshInventory?.Invoke();
            GetComponent<PlayerMovement>().enabled = false;
            await Task.Delay(700);
            GetComponent<PlayerMovement>().enabled = true;
        }
    }
    public void AddInventorySlot(int amount)
    {
        Mathf.Clamp(inventorySlot += amount, 0, 5);
        Actions.RefreshInventory?.Invoke();
    }
}
