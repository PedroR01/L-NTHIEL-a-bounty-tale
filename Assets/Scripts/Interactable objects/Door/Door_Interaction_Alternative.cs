using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door_Interaction_Alternative : MonoBehaviour
{
    //[SerializeField]private GameObject instruction;
    private bool canOpen;

    [SerializeField] private GameObject uiMessage;

    private void Start()
    {
        canOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen)
            DoorMotion();
    }

    private void DoorMotion()
    {
        Animator anim = GetComponent<Animator>();

        if (!anim.isActiveAndEnabled)
            anim.enabled = true;
        else
            anim.SetTrigger("interact");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && !Inventory.inventoryInstance.CheckInventoryFor("Key"))
        {
            if (uiMessage == null)
                uiMessage = GameObject.Find("Instructions Panel");
            else
            {
                uiMessage.GetComponent<Image>().enabled = true;
                // uiMessage.GetComponentInChildren<Text>().enabled = true;
                Text text = uiMessage.GetComponentInChildren<Text>();
                text.enabled = true;
                text.text = "You need a key";
                // uiMessage.gameObject.SetActive(true);
            }
        }
        else if (other.gameObject.layer == 10 && Inventory.inventoryInstance.CheckInventoryFor("Key"))
        {
            Debug.Log("You can open the door");
            canOpen = true;
            if (uiMessage == null)
                uiMessage = GameObject.Find("Instructions Panel");
            else
            {
                uiMessage.GetComponent<Image>().enabled = true;
                Text text = uiMessage.GetComponentInChildren<Text>();
                text.enabled = true;
                text.text = "Press ?E? to interact";
                //uiMessage.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
            canOpen = false;
        if (!uiMessage.activeInHierarchy)
            return;
        else
        {
            uiMessage.GetComponent<Image>().enabled = false;
            uiMessage.GetComponentInChildren<Text>().enabled = false;
            //uiMessage.gameObject.SetActive(false);
        }
    }
}