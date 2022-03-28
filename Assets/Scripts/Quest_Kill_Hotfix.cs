using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Kill_Hotfix : MonoBehaviour
{
    //[SerializeField] private GameObject[] enemies;
    [SerializeField] private Enemy[] isAlive;

    [SerializeField] private GameObject[] portal; //Si no funciona usar serialized field

    private bool activation;

    private void Start()
    {
        activation = false;
        /*for (int i = 0; i <= enemies.Length; i++)
            isAlive[i] = enemies[i].GetComponent<Enemy>();*/
    }

    private void Update()
    {
        if (!isAlive[0].dead || !isAlive[1].dead || !isAlive[2].dead || !isAlive[3].dead || !isAlive[4].dead)
            return;
        else if (isAlive[0].dead && isAlive[1].dead && isAlive[2].dead && isAlive[3].dead && isAlive[4].dead && !activation)
        {
            activation = true;
            for (int i = 0; i <= portal.Length - 1; i++)
            {
                portal[i].gameObject.SetActive(true);
            }
        }
    }
}