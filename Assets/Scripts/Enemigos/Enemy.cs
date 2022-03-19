using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Enemy_Data data; // Tengo que ver bien si esto iria aca o si iria en la clase hija

    protected GameObject objStats;

    protected float maxLife;
    protected float actualLife;
    protected float damage;
    protected float armour;
    protected float speed;

    protected float timeToAttack, attackTimer;
    [SerializeField] protected bool shouldPatrol;
    [SerializeField] private Enemy_Behaviour patrolScript;
    [SerializeField] private Transform originPosition;
    [SerializeField] private Transform objective;
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
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DamageReceived(float damageAmount) // Ver si este como dead deberian ser abstract o dejarlos asi. Falta la animacion al ser golpeado
    {
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
            if (patrol)
            {
                if (!patrolScript.isActiveAndEnabled)
                {
                    ReturnToOriginPosition();
                    chase = false;
                    attack = false;
                }
            }
            else if (chase && !attack)
            {
                stand = false;
                patrol = false;
                Chase();
            }
        }
    }

    private float FieldOfView()
    {
        Vector3 rayOffset = new Vector3(0, 1.3f, 0);
        Ray ray = new Ray(transform.position + rayOffset, transform.TransformDirection(Quaternion.Euler(0, 11, 0) * Vector3.forward)); // Right side line 27y
        Ray ray2 = new Ray(transform.position + rayOffset, transform.TransformDirection(Quaternion.Euler(0, -11, 0) * Vector3.forward)); // Left side line -19y
        Vector3 rayRange = new Vector3(ray.origin.x, ray.origin.y, ray.origin.z - rayMaxDistance);

        // Comente estas lineas porque no siguen el movimiento y rotacion del personaje, los ray si
        Debug.DrawLine(ray.origin, Quaternion.Euler(0, -45, 0) * rayRange, Color.red); // Right side line
        Debug.DrawLine(ray2.origin, Quaternion.Euler(0, 45, 0) * rayRange, Color.red); // Left side line

        float rayAngle = Vector3.Angle(ray.direction, ray2.direction);
        fieldOfView = rayAngle;
        return fieldOfView;
    }

    private Vector3 DirectionToObjective()
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
            /*if (Physics.Raycast(transform.position, DirectionToObjective(), out hit, rayMaxDistance, obstacleLayer)) // El error aca era porque el rayo ignora a todos los layers excepto el del obstaculo y impactaba en un uno
                return;*/
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

    private void Rotation(Vector3 direccion)
    {
        if (direccion != Vector3.zero)
        {
            float velocidadGiro = 2.5f; // Esta variable cuando la pase a el otro script, ponerla arriba del start
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionObjetivo, velocidadGiro * Time.deltaTime); // Interpola los 2 primeros valores con el ultimo y normaliza el resultado.
        }
    }

    protected float ObjectiveDistance()
    {
        return Vector3.Distance(transform.position, objective.position); //Devuelve la magnitud de la distancia entre los 2 vectores
    }

    private bool MustMove(Vector3 vectorObj)
    {
        return vectorObj.magnitude > minDistance;
    } // Booleano para que no se superponga al jugador. De esta manera se mueve siempre manteniendo una distancia minima con el objetivo.

    protected virtual void Chase()
    {
        Vector3 objectiveDirection = DirectionToObjective();
        if (ObjectiveDistance() > minDistance && ObjectiveDistance() < maxDistance)
        {
            MoveTo(objectiveDirection);
            Rotation(objectiveDirection);
        }

        if (ObjectiveDistance() > maxDistance)
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

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Origin Position")
        {
            patrol = false;
            stand = true;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}