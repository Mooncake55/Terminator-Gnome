using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform itemContainer;
    public GameObject itemPrefab;

    public void UpdateInventory(List<Item> items)
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in items)
        {
            GameObject newItemUI = Instantiate(itemPrefab, itemContainer);
            newItemUI.GetComponentInChildren<Text>().text = item.itemName;
            newItemUI.GetComponentInChildren<Image>().sprite = item.icon;
        }
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
