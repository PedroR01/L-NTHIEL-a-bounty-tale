using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Enemy_Data data;

    protected GameObject objStats;

    protected float maxLife;
    public float actualLife;
    [HideInInspector] public float damage; // Public para poder aplicarlo a la flecha
    protected float armour;
    protected float speed;

    protected float timeToAttack, attackTimer;
    [SerializeField] protected bool shouldPatrol;
    [SerializeField] protected Enemy_Behaviour patrolScript;
    public Transform originPosition;
    public Transform objective;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float fieldOfView;
    [SerializeField] private float rayMaxDistance;
    [Range(1, 5)][SerializeField] protected float minDistance;
    [SerializeField] protected float maxDistance;
    [SerializeField] protected float maxAttackDist;
    public bool stand; // Estos booleanos se relacionan a las animaciones, asi que son importantes modificarlos dependiendo de la funcion utilizada aca
    public bool patrol; // Ver si conviene cambiar los booleanos por un switch con los distintos estados
    public bool chase;
    public bool attack;
    public bool dead = false;
    protected bool damageTakedCheck;

    public enum TiposDeEnemigos
    {
        Guardia,
        Oficial,
        Arquero
    }

    protected virtual void EnemyStats() // 1 Guardian - 2 Archer
    {
        maxLife = data.life;
        actualLife = maxLife;
        damage = data.damage;
        armour = data.armour;
        speed = data.speed;
        this.gameObject.name = data.name;
    }

    public bool CanAttack() // Tiempo entre tiros o golpes
    {
        // Debug.Log("Tiempo: " + attackTimer);
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
            return true;
        else
            return false;
    }

    public void DamageReceived(float damageAmount) // Ver si este como dead deberian ser abstract o dejarlos asi. Falta la animacion al ser golpeado
    {
        damageTakedCheck = true;
        actualLife -= damageAmount - armour;
        Debug.Log("Enemy life: " + actualLife);
    }

    protected abstract void Dead();

    protected void ChangePatrolState()
    {
        SetPatrol();
    } // Activa o desactiva el script de patrullaje. Ver si esto esta de mas

    protected virtual void TheBehaviour()
    {
        if (actualLife <= 0)
        {
            Dead();
        }
        else
        {
            Raycast();
            if (patrol && !damageTakedCheck)
            {
                if (!patrolScript.isActiveAndEnabled)
                {
                    ReturnToOriginPosition();
                    chase = false;
                    attack = false;
                }
            }
            else if (chase && !attack || damageTakedCheck)
            {
                if (patrolScript.isActiveAndEnabled)
                    patrolScript.enabled = false;

                stand = false;
                patrol = false;
                chase = true;
                Chase();
            }
        }
    }

    private float FieldOfView()
    {
        Vector3 rayOffset = new Vector3(0, 1.3f, 0);
        Ray ray = new Ray(transform.position + rayOffset, transform.TransformDirection(Quaternion.Euler(0, 30, 0) * Vector3.forward)); // Right side line 27y
        Ray ray2 = new Ray(transform.position + rayOffset, transform.TransformDirection(Quaternion.Euler(0, -30, 0) * Vector3.forward)); // Left side line -19y
        Vector3 rayRange = new Vector3(ray.origin.x, ray.origin.y, ray.origin.z - rayMaxDistance);

        // Comente estas lineas porque no siguen el movimiento y rotacion del personaje, los ray si
        Debug.DrawLine(ray.origin, Quaternion.Euler(0, -45, 0) * rayRange, Color.red); // Right side line
        Debug.DrawLine(ray2.origin, Quaternion.Euler(0, 45, 0) * rayRange, Color.red); // Left side line

        float rayAngle = Vector3.Angle(ray.direction, ray2.direction);
        fieldOfView = rayAngle;
        return fieldOfView;
    }

    protected Vector3 DirectionToObjective()
    {
        Vector3 direccion = objective.position - transform.position;
        direccion.y = 0;

        return direccion;
    }

    private void Raycast()
    {
        FieldOfView();
        RaycastHit hit;
        float objAngle = Vector3.Angle(DirectionToObjective(), this.transform.forward);

        if (objAngle <= fieldOfView)
        {
            if (Physics.Raycast(transform.position, DirectionToObjective(), out hit, rayMaxDistance))
            {
                if (hit.collider.tag == "Player")
                {
                    patrol = false;
                    if (!attack)
                        chase = true;
                }
                else if (hit.collider.tag == "Obstacle")
                    patrol = true;
            }
        }
    } // Despues ver de tanto el raycast como el fov ponerlos en un script aparte

    private void MoveTo(Vector3 vectorObj)
    {
        transform.Translate(vectorObj.normalized * speed * Time.deltaTime, Space.World);
    }

    protected void Rotation(Vector3 direccion)
    {
        if (direccion != Vector3.zero)
        {
            float velocidadGiro = 10f;
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionObjetivo, velocidadGiro * Time.deltaTime); // Interpola los 2 primeros valores con el ultimo y normaliza el resultado.
        }
    }

    protected float ObjectiveDistance()
    {
        return Vector3.Distance(transform.position, objective.position); //Devuelve la distancia entre los 2 vectores
    }

    private bool MustMove(Vector3 vectorObj)
    {
        return vectorObj.magnitude > minDistance;
    } // Booleano para que no se superponga al jugador. De esta manera se mueve siempre manteniendo una distancia minima con el objetivo.

    protected virtual void Chase()
    {
        Vector3 objectiveDirection = DirectionToObjective();
        if (ObjectiveDistance() > maxAttackDist && ObjectiveDistance() < maxDistance || damageTakedCheck)
        {
            //patrol = false;
            //stand = false;
            MoveTo(objectiveDirection);
            Rotation(objectiveDirection);
        }
        else if (ObjectiveDistance() > maxDistance)
        {
            patrol = true;
            chase = false;
        }
    }

    private void ReturnToOriginPosition()
    {
        //Vector3 originWorldPosition = originPosition.TransformPoint(originPosition.position);
        MoveTo(originPosition.position - transform.position);
        Rotation(originPosition.position - transform.position);
    }

    private void SetPatrol()
    {
        patrolScript.enabled = !patrolScript.isActiveAndEnabled;
    }

    private IEnumerator returnToPlace()
    {
        yield return new WaitForSeconds(5);
        if (!chase)
        {
            damageTakedCheck = false;
            patrol = true;
        }
    }
}