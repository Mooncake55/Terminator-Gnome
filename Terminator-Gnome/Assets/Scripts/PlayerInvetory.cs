using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventory;
    private Item nearbyItem;

    public System.Collections.Generic.List<Item> items = new System.Collections.Generic.List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"Picked up item: {item.itemName}");
    }

    private void Update()
    {
        if (nearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(nearbyItem);
            Destroy(nearbyItem.gameObject);
            nearbyItem = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Item>())
        {
            nearbyItem = other.GetComponent<Item>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Item>() == nearbyItem)
        {
            nearbyItem = null;
        }
    }
}
