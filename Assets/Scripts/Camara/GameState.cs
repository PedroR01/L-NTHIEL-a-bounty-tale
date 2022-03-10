using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private bool isPaused;
    private bool gameOver;

    private void Start()
    {
        Game_Manager.gameInstance.SetGameState(Game_Manager.gameState.Play);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Game_Manager.gameInstance.SetGameState(Game_Manager.gameState.Pause);
    }

    private void StateOfGame(Game_Manager.gameState state)
    {
        Game_Manager.gameInstance.SetGameState(state);
    }
}