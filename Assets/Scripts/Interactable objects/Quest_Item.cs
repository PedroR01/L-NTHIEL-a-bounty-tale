using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Item : MonoBehaviour
{
    public bool questItem;
    private bool questInstruction;
    [SerializeField] private GameObject uiMessage;

    private void Start()
    {
        questItem = false;
        questInstruction = true;
    }

    private void Update()
    {
        if (!Inventory.inventoryInstance.CheckInventoryFor("Treasure"))
            return;
        else if (Inventory.inventoryInstance.CheckInventoryFor("Treasure") && questInstruction)
            QuestUpdated();
    }

    private void QuestUpdated()
    {
        if (uiMessage.activeInHierarchy)
            return;
        else
        {
            Text text = uiMessage.GetComponentInChildren<Text>();
            text.text = "ESCAPE NOW! THE GUARDS ARE AFTER YOU";
            uiMessage.gameObject.SetActive(true);
            questItem = true;

            StartCoroutine(InstructionsTime());
        }
    }

    private IEnumerator InstructionsTime()
    {
        yield return new WaitForSeconds(2f);
        uiMessage.gameObject.SetActive(false);
        questInstruction = false;
    }
}