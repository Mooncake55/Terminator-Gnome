using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject itemPrefab;
    public string itemName;
    public Sprite icon;

    //public void Use()
    //{
    //    Debug.Log("Usando el objeto: " + itemName);
    //}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(this);
                Destroy(gameObject);
            }
        }
    }
}

