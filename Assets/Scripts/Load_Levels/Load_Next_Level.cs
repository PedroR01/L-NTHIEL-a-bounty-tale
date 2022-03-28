using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Next_Level : MonoBehaviour
{
    private Scene actualScene;
    private Player_Data sendData;
    public bool gameFinished;

    private void Start()
    {
        actualScene = SceneManager.GetActiveScene();
        sendData = FindObjectOfType<Player_Data>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && actualScene.name == "Main Menu")
        {
            gameFinished = false;
            SceneManager.LoadScene("Dungeon_Level1");
        }

        if (other.gameObject.layer == 10 && actualScene.name == "Dungeon_Level1")
        {
            sendData.getDataBeforeLoad = true;
            SceneManager.LoadScene("Village_Level2");
        }

        if (other.gameObject.layer == 10 && actualScene.name == "Village_Level2")
        {
            sendData.getDataBeforeLoad = true;
            SceneManager.LoadScene("Castle_Level3");
        }

        if (other.gameObject.layer == 10 && actualScene.name == "Castle_Level3")
        {
            gameFinished = true;
            sendData.getDataBeforeLoad = true;
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void OnQuit()
    {
        Debug.Log("Aplication Closed");
        Application.Quit();
    }
}