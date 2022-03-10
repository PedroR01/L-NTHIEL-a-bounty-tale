using UnityEngine;
using UnityEngine.UI;

public class Inventory_Slot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button removeButton;

    private Scriptable_Item itemPicked;

    public void AddItem(Scriptable_Item newItem)
    {
        itemPicked = newItem;

        icon.sprite = itemPicked.icon;
        icon.enabled = true;

        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        itemPicked = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    public void OnUseButtonClicked()
    {
        if (itemPicked != null)
            itemPicked.Use();
    }

    public void OnRemoveButtonClicked()
    {
        Inventory.inventoryInstance.RemoveFromInventory(itemPicked);
    }
}