using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : MonoBehaviour
{
    public Player_Stats playerData;
    public Bow arrowData;
    public float lifeData;
    public float staminaData;
    public float munitionData;
    public bool getDataBeforeLoad;

    private void Start()
    {
        playerData = FindObjectOfType<Player_Stats>();
        arrowData = FindObjectOfType<Bow>();
        getDataBeforeLoad = false;

        //Ver si esto hace falta porque va en el gamemanager y tiene dontdestroyonload
        lifeData = 0;
        staminaData = 0;
        munitionData = 0;
    }

    private void Update()
    {
        if (getDataBeforeLoad) //booleano pra cuando este por pasar de nivel
        {
            lifeData = playerData.lastLifeValue;
            staminaData = playerData.lastStaminaValue;
            munitionData = arrowData.lastMunitionCheck;

            getDataBeforeLoad = false;
        }
    }
}