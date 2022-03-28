using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Dialogue : MonoBehaviour
{
    public GameObject[] dialoguePanel; // Paneles de informacion de mision y distintas opciones
    public GameObject[] interact; // Ui de icono de mision disponible y instruccion para interactuar
    public Button[] dialogueButtons; // Botones con las distintas opciones a elegir para la mision
    public Text[] dialogueTexts;  // Carga los strings del dictionary y los incluye como texto en los botones

    private bool talking;
    private bool accepted;

    public Dictionary<QuestAction, string> dialogues;

    private void Start()
    {
        dialogues = new Dictionary<QuestAction, string>();
        dialogues.Add(QuestAction.Accept, "Let's go");
        dialogues.Add(QuestAction.Reject, "I don't care");
        dialogues.Add(QuestAction.Postpone, "Maybe later");
        talking = false;
        accepted = false;
    }

    private void Update()
    {
        if (accepted && interact[1].activeInHierarchy)
            return;
        if (!talking && interact[1].activeInHierarchy && Input.GetKeyDown(KeyCode.E))
            talking = true;
    }

    private void UiInteractionMessage()
    {
        interact[1].SetActive(true);
        Text interactMessage = interact[1].GetComponentInChildren<Text>();
        interactMessage.text = "Press ´E´ to talk";
    }

    private void LoadDialogues()
    {
        int buttonIndex = 0;
        foreach (var dialogue in dialogues.Keys)
        {
            dialogueButtons[buttonIndex].onClick.RemoveAllListeners();
            dialogueButtons[buttonIndex].onClick.AddListener(() => { OnClicked(dialogue); });

            string message;
            dialogues.TryGetValue(dialogue, out message);
            dialogueTexts[buttonIndex].text = message;

            buttonIndex++;
        }

        Text questMessage = dialoguePanel[0].GetComponentInChildren<Text>();
        questMessage.text = "Some soldiers take our village, please help us. You can use the teleporter once you finished them";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (accepted)
            return;

        UiInteractionMessage();
    }

    private void OnTriggerStay(Collider other)
    {
        if (accepted && !interact[0].activeInHierarchy)
            return;

        if (talking && !accepted)
        {
            Cursor.lockState = CursorLockMode.None;
            dialoguePanel[0].SetActive(true);
            dialoguePanel[1].SetActive(true);
            LoadDialogues();
            talking = false;
        }

        if (accepted)
        {
            interact[0].SetActive(false);
            interact[1].SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Cursor.lockState = CursorLockMode.Locked;
        interact[1].SetActive(false);
        dialoguePanel[0].SetActive(false);
        dialoguePanel[1].SetActive(false);

        talking = false;
    }

    public void OnClicked(QuestAction action)
    {
        switch (action)
        {
            case QuestAction.Accept:
                Debug.Log("Quest Accepted");
                Cursor.lockState = CursorLockMode.Locked;
                accepted = true;
                break;

            case QuestAction.Reject:
                Debug.Log("Quest Rejected");
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case QuestAction.Postpone:
                Debug.Log("Quest Postponed");
                Cursor.lockState = CursorLockMode.Locked;
                break;

            default:
                break;
        }
        dialoguePanel[0].SetActive(false);
        dialoguePanel[1].SetActive(false);
    }
}

public enum QuestAction
{
    Accept,
    Reject,
    Postpone
}