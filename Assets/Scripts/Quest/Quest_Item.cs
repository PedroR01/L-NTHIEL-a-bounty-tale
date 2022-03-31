using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Item : MonoBehaviour
{
    public bool questItem;
    private bool questInstruction;
    private GameObject uiMessage;
    [SerializeField] private Load_Next_Level canLeave;

    private void Start()
    {
        uiMessage = GameObject.Find("Instructions Panel");

        questItem = false;
        questInstruction = true;
    }

    private void Update()
    {
        if (!Inventory.inventoryInstance.CheckInventoryFor("Treasure"))
            return;
        else if (Inventory.inventoryInstance.CheckInventoryFor("Treasure") && questInstruction)
        {
            canLeave.enabled = true;
            QuestUpdated();
        }
    }

    private void QuestUpdated()
    {
        Text text = uiMessage.GetComponentInChildren<Text>();
        uiMessage.GetComponent<Image>().enabled = true;
        text.enabled = true;
        text.fontSize = 50;
        text.text = "ESCAPE NOW! THE GUARDS ARE AFTER YOU";
        //uiMessage.gameObject.SetActive(true);
        canLeave.enabled = true;
        questItem = true;

        StartCoroutine(InstructionsTime());
    }

    private IEnumerator InstructionsTime()
    {
        yield return new WaitForSeconds(3f);
        uiMessage.GetComponent<Image>().enabled = false;
        uiMessage.GetComponentInChildren<Text>().enabled = false;
        //uiMessage.gameObject.SetActive(false);
        questInstruction = false;
        questItem = false;
    }
}