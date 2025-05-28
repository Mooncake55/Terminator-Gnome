using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public InventoryUI inventoryUI;

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log("Objeto recogido: " + item.itemName);
        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventory(items);
        }
    }

}

