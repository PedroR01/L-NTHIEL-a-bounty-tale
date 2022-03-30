using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //private Cinemachine.CinemachineImpulseSource source;

    private Rigidbody rb;

    private float damage;
    private float velocity;
    private bool hit;
    private float torque;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        damage = 57f;
        velocity = 1.5f;
        torque = 2f;
    }

    public void Fly(Vector3 force)
    {
        rb.AddForce(force / velocity, ForceMode.Force); //A�adir a aca que el rb use la gravedad (para que se active al dispararse)
        //rb.AddTorque(transform.right * torque);
        /* source = GetComponent<Cinemachine.CinemachineImpulseSource>();
        source.GenerateImpulse(Camera.main.transform.forward); */ // Seria una especie de recoil. Para ajustarlo mejor ir a su componente en el prefab
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        rb.useGravity = true;
        transform.parent = null;
    }

    private void OnCollisionEnter(Collision c1)
    {
        if (hit) return;
        hit = true;

        if (c1.gameObject.layer == 9) // Enemies layer
        {
            var enemyHealth = c1.gameObject.GetComponent<Enemy>();
            enemyHealth.DamageReceived(damage);
            transform.SetParent(c1.transform);
            rb.isKinematic = true;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;

            Destroy(this.gameObject, 2);
        }
        else if (c1.gameObject.layer == 7 || c1.gameObject.layer == 6)
        {
            transform.SetParent(c1.transform);
            rb.isKinematic = true;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
        else if (c1.gameObject.layer != 10) // Player layer
        {
            transform.SetParent(c1.transform);
            GetComponent<PhysicMaterial>().bounciness = 0.3f;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}