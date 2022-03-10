using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    // Ver despues si esta bien tener 2 singletons en un game manager y si puedo/hace falta cambiar la list por un array

    #region Singleton

    public static Inventory inventoryInstance;
    public List<Scriptable_Item> items = new List<Scriptable_Item>();

    private void Awake()
    {
        if (inventoryInstance != null)
        {
            Debug.LogWarning("You have more than 1 instance of Inventory class");
            return;
        }
        inventoryInstance = this;
    }

    #endregion Singleton

    public delegate void OnItemChanged(); // Se recomienda siempre usar delegate junto con Event por seguridad y optimizacion.

    public event OnItemChanged itemChangedHandler;

    [SerializeField][Range(2, 18)] private int space = 18;

    public bool AddToInventory(Scriptable_Item item)
    {
        //  if(!item.isDefaultItem)
        if (items.Count >= space)
        {
            Debug.Log("Inventory full! Please drop an item if you want to pick another");
            return false;
        }
        items.Add(item);
        itemChangedHandler?.Invoke();

        return true;
    }

    public void RemoveFromInventory(Scriptable_Item item)
    {
        items.Remove(item);
        itemChangedHandler?.Invoke();
    }
}