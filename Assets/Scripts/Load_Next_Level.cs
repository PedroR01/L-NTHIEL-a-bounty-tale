using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Next_Level : MonoBehaviour
{
    private Scene actualScene;

    private void Start()
    {
        actualScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && actualScene.name == "Main Menu")
            SceneManager.LoadScene("Dungeon_Level1");

        if (other.gameObject.layer == 10 && actualScene.name == "Dungeon_Level1")
            SceneManager.LoadScene("Village_Level2");

        if (other.gameObject.layer == 10 && actualScene.name == "Village_Level2")
            SceneManager.LoadScene("Castle_Level3");
    }

    public void OnQuit()
    {
        Debug.Log("Aplication Closed");
        Application.Quit();
    }
}