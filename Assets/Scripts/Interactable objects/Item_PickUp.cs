using UnityEngine;

public class Item_PickUp : Interactable
{
    [SerializeField] private Scriptable_Item item;

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
            Destroy(gameObject);
    }
}