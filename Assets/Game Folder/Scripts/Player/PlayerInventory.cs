using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventorySlot = 1;
    [SerializeField] private List<GameObject> inventory = new List<GameObject>();
    [SerializeField] private Transform inventoryStorage;
    private void OnEnable()
    {
        Actions.AddObjectToinventory += AddObjectToInventory;
        Actions.ThrowGarbage += ThrowGarbage;
    }


    private void OnDisable()
    {
        Actions.AddObjectToinventory -= AddObjectToInventory;
        Actions.ThrowGarbage -= ThrowGarbage;
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
    }

    private void AddObjectToInventory(GameObject obj)
    {
        if (inventory.Count < inventorySlot)
        {
            AudioManager.instance.PlayMusic("Pickup");
            obj.transform.SetParent(inventoryStorage,false);
            inventory.Add(obj);
        }
    }
    public void AddInventorySlot(int amount)
    {
        inventorySlot += amount;
    }
}
