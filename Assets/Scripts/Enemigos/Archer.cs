using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Transform prefabSpawn;

    private void Start()
    {
        damageTakedCheck = false;
        EnemyStats();
        if (shouldPatrol)
        {
            ChangePatrolState();
            shouldPatrol = false;
        }
        //objStats = GameObject.FindGameObjectWithTag("Player");

        timeToAttack = 7f;
        attackTimer = timeToAttack;
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
        Rotation(DirectionToObjective());
        if (ObjectiveDistance() < maxAttackDist && CanAttack())
        {
            Rigidbody rb = Instantiate(prefab, prefabSpawn.position, prefabSpawn.rotation).GetComponent<Rigidbody>();
            Debug.Log("Distancia al objetivo: " + ObjectiveDistance());
            rb.AddForce(transform.forward * ObjectiveDistance() * 50f, ForceMode.Force);
            rb.AddForce(new Vector3(0, ObjectiveDistance() * 15f, 0), ForceMode.Force);

            // rb.AddForce(transform.forward, ForceMode.Force);
            // rb.AddForce(transform.up * 8f, ForceMode.Force);
            //A�adir particulas para saber donde va la flecha

            if (CanAttack())
                attackTimer = timeToAttack;
        }
    }

    protected override void Chase()
    {
        base.Chase();
        if (ObjectiveDistance() > minDistance && ObjectiveDistance() < maxAttackDist)
        {
            Debug.Log("Archer chase state");
            attack = true;
            patrol = false;
            chase = false;
        }
    }

    protected override void Dead()
    {
        dead = true;
        Destroy(GetComponent<Rigidbody>());
        Destroy(this, 3);
    }
}