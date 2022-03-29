using UnityEngine;
using UnityEngine.UI;

public class Item_PickUp : Interactable
{
    [SerializeField] private Scriptable_Item item;
    //[SerializeField] private GameObject uiMessage;

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
            uiMessage.GetComponent<Image>().enabled = false;
            uiMessage.GetComponentInChildren<Text>().enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && uiMessage.activeInHierarchy)
        {
            uiMessage.GetComponent<Image>().enabled = true;
            Text text = uiMessage.GetComponentInChildren<Text>();
            text.enabled = true;
            text.text = "Press ´E´ to pick up " + item.name;
            //uiMessage.gameObject.SetActive(true);
        }
        if (other.gameObject.layer == 10 && Input.GetKeyDown(KeyCode.E)) // Ver por que no funciona.
        {
            bool canPickUp = Inventory.inventoryInstance.AddToInventory(item);

            if (canPickUp)
            {
                if (item.isKey)
                {
                    //uiMessage.gameObject.SetActive(false);
                    Destroy(gameObject);
                }
                Destroy(gameObject); // Aca hay codigo repetido, cambiarlo despues
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
            {
                uiMessage.GetComponent<Image>().enabled = false;
                uiMessage.GetComponentInChildren<Text>().enabled = false;
            }
        }
    }
}