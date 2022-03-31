using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load_Next_Level : MonoBehaviour
{
    protected Scene actualScene;
    private Player_Data sendData;

    private void Awake()
    {
        actualScene = SceneManager.GetActiveScene();
        sendData = FindObjectOfType<Player_Data>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && actualScene.name == "Main Menu")
        {
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
            StartCoroutine(WaitToLoad());
        }
    }

    private IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(5);
        GameObject _manager = FindObjectOfType<Game_Manager>().gameObject;
        GameObject _hud = FindObjectOfType<Inventory_Ui>().gameObject;
        Destroy(_manager);
        Destroy(_hud);
        SceneManager.LoadScene("Main Menu");
    }

    public void OnQuit()
    {
        Debug.Log("Aplication Closed");
        Application.Quit();
    }
}