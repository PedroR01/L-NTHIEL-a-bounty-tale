using UnityEngine;
using UnityEngine.UI;

public class Inventory_Ui : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUi;
    [SerializeField] private Transform itemsParent;
    private Inventory inventory;
    private Inventory_Slot[] slots;

    private void Start()
    {
        inventory = Inventory.inventoryInstance;
        Subscribe();

        slots = itemsParent.GetComponentsInChildren<Inventory_Slot>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
            inventoryUi.SetActive(!inventoryUi.activeSelf);
    }

    private void UpdateUi()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
                slots[i].AddItem(inventory.items[i]);
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    private void Subscribe()
    {
        inventory.itemChangedHandler += UpdateUi;
    }

    private void Unsubscribe()
    {
        inventory.itemChangedHandler -= UpdateUi;
    }

    private void OnDestroy() // No se me ocurrio en que otro caso desuscribirlo por ahora.
    {
        Unsubscribe();
    }
}