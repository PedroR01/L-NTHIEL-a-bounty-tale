using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Objective : MonoBehaviour
{
    protected float objectiveAmount;
    protected float amountCount;
    protected bool objectiveCompleted = false;

    protected void MissionType(string type)
    {
        if (type == "Item")
            Debug.Log("Item Quest");

        if (type == "Kill")
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            objectiveAmount = enemies.Length;

            /*for (int i = 0; i <= objectiveAmount; i++)
            {
                amountCount = enemies[i].GetKillCount();
            }*/

            if (amountCount >= objectiveAmount)
            {
                objectiveCompleted = true;
                Debug.Log("Kill Quest");
            }
        }
    }
}