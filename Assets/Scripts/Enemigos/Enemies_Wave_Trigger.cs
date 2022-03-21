using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Wave_Trigger : MonoBehaviour
{
    private Quest_Item waveTrigger;
    private Vector3 origin;
    private GameObject[] enemies;

    private void Start()
    {
        waveTrigger = GameObject.Find("Quest Trigger").GetComponent<Quest_Item>();
        enemies = GameObject.FindGameObjectsWithTag("Guardian");
    }

    private void Update()
    {
        if (!waveTrigger.questItem)
            return;
        else
            for (int i = 0; i < enemies.Length; i++)
            {
                origin = enemies[i].GetComponent<Guardian>().originPosition.position;
                enemies[i].transform.position = origin;
                enemies[i].GetComponent<Guardian>().chase = true;
            }
    }
}