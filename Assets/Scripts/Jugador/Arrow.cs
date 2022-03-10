using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Cinemachine.CinemachineImpulseSource source;

    private Rigidbody rb;

    private float damage;
    private float velocity;
    private bool hit;
    private float torque;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        damage = 25f;
        velocity = 30f;
        torque = 2f;
    }

    public void Fly(Vector3 force)
    {
        rb.AddForce(force / velocity, ForceMode.Impulse); //Añadir a aca que el rb use la gravedad (para que se active al dispararse)
        rb.AddTorque(transform.right * torque);
        source = GetComponent<Cinemachine.CinemachineImpulseSource>();
        source.GenerateImpulse(Camera.main.transform.forward); // Seria una especie de recoil. Para ajustarlo mejor ir a su componente en el prefab
        rb.useGravity = true;
        transform.SetParent(null);
        //Destroy(this.gameObject, 2);
    }

    private void OnCollisionEnter(Collision c1)
    {
        if (hit) return;
        hit = true;

        if (c1.gameObject.layer == 9) // Enemies layer
        {
            var enemyHealth = c1.gameObject.GetComponent<Enemy>();
            enemyHealth.DamageReceived(damage);
            Destroy(this.gameObject, 5);
        }

        if (c1.gameObject.layer != 10) // Player layer
        {
        }

        /* rb.velocity = Vector3.zero;
         rb.angularVelocity = Vector3.zero;
         transform.SetParent(c1.transform);
         rb.isKinematic = true;*/
    }
}