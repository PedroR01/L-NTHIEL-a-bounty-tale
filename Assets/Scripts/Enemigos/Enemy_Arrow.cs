using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Arrow : MonoBehaviour
{
    private Enemy damageData;
    private float arrowDamage;
    private bool alreadyHit;

    private void Start()
    {
        damageData = FindObjectOfType<Archer>();
        arrowDamage = damageData.damage;
        alreadyHit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10 && !alreadyHit) //
        {
            collision.gameObject.GetComponent<Player_Stats>().RecibirDanio(arrowDamage);
            alreadyHit = true;
        }
    }
}