using UnityEngine;
using UnityEngine.UI;

public class Item_PickUp : Interactable
{
    [SerializeField] private Scriptable_Item item;
    [SerializeField] private GameObject uiMessage;

    protected override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        bool canPickUp = Inventory.inventoryInstance.AddToInventory(item);

        if (canPickUp)
        {
            uiMessage.gameObject.SetActive(false);
            if (item.isKey)

                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (uiMessage.activeInHierarchy)
                return;
            else
            {
                Text text = uiMessage.GetComponentInChildren<Text>();
                text.text = "Press ´E´ to pick up " + item.name;
                uiMessage.gameObject.SetActive(true);
            }
        }
        if (other.gameObject.layer == 10 && Input.GetKeyDown(KeyCode.E)) // Ver por que no funciona.
        {
            bool canPickUp = Inventory.inventoryInstance.AddToInventory(item);

            if (canPickUp)
            {
                if (item.isKey)
                {
                    uiMessage.gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (!uiMessage.activeInHierarchy)
                return;
            else
                uiMessage.gameObject.SetActive(false);
        }
    }
}