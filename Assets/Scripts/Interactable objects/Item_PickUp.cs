using UnityEngine;
using UnityEngine.UI;

public class Item_PickUp : MonoBehaviour
{
    [SerializeField] private Scriptable_Item item;
    [SerializeField] protected float radius = 2.5f; // Distancia para que el player interactua
    [SerializeField] protected Transform interactionTransform;
    [SerializeField] private Transform player;
    protected GameObject uiMessage;
    protected bool inRadius;

    public bool canInteract = false;

    //[SerializeField] private GameObject uiMessage;

    private void Start()
    {
        if (uiMessage == null)
            uiMessage = GameObject.Find("Instructions Panel");
    }

    private void Update()
    {
        if (inRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp();
                canInteract = true;
            }
        }
        else if (inRadius)
        {
            canInteract = false;
        }
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
        if (other.gameObject.layer == 10)
        {
            inRadius = true;
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
                inRadius = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            inRadius = false;
            uiMessage.GetComponent<Image>().enabled = false;
            uiMessage.GetComponentInChildren<Text>().enabled = false;
        }
    }
}