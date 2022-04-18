using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private bool isPaused;
    private bool gameOver;

    private void Start()
    {
        isPaused = false;
        gameOver = false;
        StateOfGame("Play");
    }

    private void Update()
    {
        ChangingStates();
    }

    public void StateOfGame(string stateName)
    {
        Game_Manager.gameState state = (Game_Manager.gameState)System.Enum.Parse(typeof(Game_Manager.gameState), stateName); // Convierte el parametro de tipo string al Enum de gameState || Seria como lo contrario a un ToString()
        Game_Manager.gameInstance.SetGameState(state);

        if (Game_Manager.gameInstance.GetGameState() == Game_Manager.gameState.Play)
        {
            Debug.LogWarning("Playing");
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }

        if (Game_Manager.gameInstance.GetGameState() == Game_Manager.gameState.Pause)
        {
            Debug.LogWarning("Paused");
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            //filtro de postprocessing para que se entienda que se paro el tiempo
        }

        if (Game_Manager.gameInstance.GetGameState() == Game_Manager.gameState.GameOver)
        {
            Debug.LogWarning("Dead");
            gameOver = true;
            Time.timeScale = 0.5f;
            Restart();
        }
    }

    private void ChangingStates()
    {
        if (Input.GetKeyDown(KeyCode.P) && Game_Manager.gameInstance.GetGameState() == Game_Manager.gameState.Play)
            StateOfGame("Pause");
        else if (Input.GetKeyDown(KeyCode.P) && Game_Manager.gameInstance.GetGameState() != Game_Manager.gameState.Play)
            StateOfGame("Play");
    }

    private void Restart()
    {
        gameOver = false;
        //camara apuntando al personaje
        //efecto de postprocesado para cambiar colores al morir

        StartCoroutine(Restarting());
    }

    private IEnumerator Restarting()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}