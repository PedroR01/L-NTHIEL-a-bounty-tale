using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager gameInstance;

    public enum mainSkillTree
    {
        Agility,
        Stealth,
        Focus
    }

    public enum gameState
    {
        Play,
        Pause,
        GameOver
    }

    private mainSkillTree skill;
    private gameState stateOfGame;

    private void Awake()
    {
        if (gameInstance != null && gameInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        gameInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSkillTree(mainSkillTree _skill)
    {
        skill = _skill;
    }

    public mainSkillTree GetSkillTree()
    {
        return skill;
    }

    public void SetGameState(gameState actualState)
    {
        stateOfGame = actualState;
    }

    public gameState GetGameState()
    {
        return stateOfGame;
    }
}