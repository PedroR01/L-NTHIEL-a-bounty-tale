using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Transform prefabSpawn;

    private void Start()
    {
        EnemyStats();
        if (shouldPatrol)
        {
            ChangePatrolState();
            shouldPatrol = false;
        }
        objStats = GameObject.FindGameObjectWithTag("Player");

        attackTimer = 3f;
        timeToAttack = attackTimer;
    }

    private void Update()
    {
        TheBehaviour();
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
        Debug.Log("FIRE!");
        if (ObjectiveDistance() < maxAttackDist && CanAttack())
        {
            Rigidbody rb = Instantiate(prefab, prefabSpawn.position, prefabSpawn.rotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 3f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //Añadir particulas para saber donde va la flecha

            if (CanAttack())
                attackTimer = timeToAttack;
        }
    }

    protected override void Chase()
    {
        base.Chase();
        if (ObjectiveDistance() > minDistance && ObjectiveDistance() < maxAttackDist)
        {
            Debug.Log("aaaaa");
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