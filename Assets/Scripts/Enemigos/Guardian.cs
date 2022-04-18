using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : Enemy
{
    private void Start()
    {
        damageTakedCheck = false;
        EnemyStats();
        maxAttackDist = minDistance + 1.3f;
        if (shouldPatrol)
        {
            ChangePatrolState();
            shouldPatrol = false;
        } // Esto nose si deberia ir en un start de la clase padre o en cada clase hijo.

        objStats = GameObject.FindGameObjectWithTag("Player");

        attackTimer = 1f;
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
        if (ObjectiveDistance() > maxAttackDist && CanAttack())
        {
            StartCoroutine(ChaseDelay());
        }
    }

    protected override void Chase()
    {
        base.Chase();
        if (ObjectiveDistance() >= minDistance && ObjectiveDistance() < maxAttackDist)
        {
            damageTakedCheck = false;
            attack = true;
            patrol = false;
            chase = false;
        }
    }

    protected override void Dead()
    {
        dead = true;
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(this);
    }

    private IEnumerator ChaseDelay()
    {
        yield return new WaitForSeconds(0.6f);
        chase = true;
        patrol = false;
        attack = false;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && CanAttack())
        {
            objStats.GetComponent<Player_Stats>().RecibirDanio(damage);
            Debug.Log("Vida actual del player" + objStats.GetComponent<Player_Stats>().actualLife + " Danio hecho: " + damage);
            attackTimer = timeToAttack;
        }
        if (other.gameObject.name == "Origin Position" && !patrolScript.isActiveAndEnabled)
        {
            patrol = false;
            stand = true;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}