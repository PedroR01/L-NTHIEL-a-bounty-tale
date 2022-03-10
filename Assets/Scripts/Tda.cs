using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tda : MonoBehaviour
{
    private enum QuestAction
    {
        Accept,
        Decline,
        Postpone
    }

    public GameObject enemy;
    public bool debugFirstEnemies;
    private Dictionary<QuestAction, string> dialogues;
    public string[] inventoryItems;
    private bool pickUp;
    private int itemsPicked;

    private void Start()
    {
        dialogues = new Dictionary<QuestAction, string>();
        inventoryItems = new string[10];
        pickUp = false;
        itemsPicked = 0;

        TheDictionary();
        TheArray();
    }

    private void Update()
    {
        StartToEnd();
        EndToStart();
    }

    private void TheDictionary()
    {
        dialogues.Add(QuestAction.Accept, "Accept mission");
        dialogues.Add(QuestAction.Decline, "Decline mission");
        dialogues.Add(QuestAction.Postpone, "I need to think");
    } // Posibles respuestas al dialogo con un npc

    private void TheArray()
    {
        string[] randomItems = new string[5];
        randomItems[0] = "Life potion";
        randomItems[1] = "Mana potion";
        randomItems[2] = "Energy potion";
        randomItems[3] = "Sword";
        randomItems[4] = "Shield";

        string pickRandomItems = randomItems[Random.Range(0, randomItems.Length)];

        if (pickUp && itemsPicked < inventoryItems.Length)
        {
            for (int i = itemsPicked; i <= itemsPicked; i++)
            {
                inventoryItems[itemsPicked] = pickRandomItems;
                Debug.Log(inventoryItems[i]);
                Debug.Log("Items picked" + itemsPicked);
            }
            itemsPicked++;
            pickUp = false;
        }
        else if (itemsPicked >= inventoryItems.Length)
        {
            Debug.Log("Inventory full. Please throw or sell some items.");
        }
    } // Inventario.

    private void StartToEnd() // Recorrer datos del principio al final
    {
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Dictionary
        {
            if (dialogues.Count == 0)
            {
                dialogues.Add(QuestAction.Accept, "Accept mission");
                dialogues.Add(QuestAction.Decline, "Decline mission");
                dialogues.Add(QuestAction.Postpone, "I need to think");
            }
            string message;

            foreach (QuestAction decision in dialogues.Keys)
            {
                dialogues.TryGetValue(decision, out message);
                Debug.Log(message);
            }
            Debug.Log("Dialogues amount options: " + dialogues.Count);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // Array
        {
            pickUp = true;
            TheArray();
        }
    }

    private void EndToStart() // Recorrer datos del final al principio
    {
        if (Input.GetKeyDown(KeyCode.Alpha5)) // Dictionary
        {
            dialogues.Clear();
            Debug.Log("Amount of dialogues left: " + dialogues.Count);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) // Array
        {
            itemsPicked--;
            Debug.Log("Se ha descartado el siguiente item: " + inventoryItems[itemsPicked]);
            inventoryItems[itemsPicked] = null;
        }
    }
}