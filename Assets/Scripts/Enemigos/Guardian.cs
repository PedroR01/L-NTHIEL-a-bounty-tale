using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : Enemy
{
    private void Start()
    {
        EnemyStats();
        maxAttackDist = minDistance;
        if (shouldPatrol)
        {
            ChangePatrolState();
            shouldPatrol = false;
        } // Esto nose si deberia ir en un start de la clase padre o en cada clase hijo.

        objStats = GameObject.FindGameObjectWithTag("Player");

        attackTimer = 2f;
        timeToAttack = attackTimer;
    }

    private void Update()
    {
        TheBehaviour();
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("life: " + maxLife + " Damage: " + damage + " Armour: " + armour + " Speed: " + speed);
        }
    }

    protected override void TheBehaviour()
    {
        base.TheBehaviour();
        if (attack)
        {
            Attack();
        }
    }

    protected void Attack()
    {
        if (ObjectiveDistance() < maxAttackDist && CanAttack() == true)
        {
            objStats.GetComponent<Player_Stats>().RecibirDanio(damage);
            Debug.Log("Vida actual del player" + objStats.GetComponent<Player_Stats>().actualLife + " Danio hecho: " + damage);
            if (CanAttack())
                attackTimer = timeToAttack;
        }
        else if (ObjectiveDistance() > maxAttackDist)
        {
            chase = true;
            patrol = false;
            attack = false;
        }
    }

    protected override void Chase()
    {
        base.Chase();
        if (ObjectiveDistance() < minDistance)
        {
            attack = true;
            patrol = false;
            chase = false;
        }
    }

    protected override void Dead()
    {
        Destroy(this, 3);
    }
}