using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions : MonoBehaviour
{
    private Player_Stats stats;
    private PlayerController pc;

    private float pushPower = 2f;
    private bool potRest;
    private float timerToRest;

    private void Start()
    {
        stats = GetComponent<Player_Stats>();
        pc = GetComponent<PlayerController>();

        potRest = false;
    }

    private void Update()
    {
        InteractableObjects();
    }

    private void InteractableObjects()
    {
        if (Input.GetKeyDown(KeyCode.E) && potRest == true)
        {
            Debug.Log("Checkpoint saved");
        } // Pot to save checkpoint

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 rayOffset = new Vector3(0, 0.3f, 0); // Buscar alguna otra forma de obtener su componente sin usar el ray. Puede ser con un OnTrigger
            Ray ray = new Ray(transform.position + rayOffset, transform.TransformDirection(Quaternion.Euler(0, 11, 0) * Vector3.forward)); ;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null && interactable.canInteract)
                {
                    Debug.Log("Item picked");
                }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // ---- Objetos interactuables ---- //

        if (hit.gameObject.layer == 20)
        {
            stats.RecibirDanio(10);
            Debug.Log("Estas recibiendo danio a causa de la lava");
        } // Danio por entrar en la lava

        if (hit.gameObject.layer == 8)
        {
            timerToRest = 1f;
            potRest = true;
        } // Colision con caldera para guardar partida
        else
        {
            if (timerToRest > 0)
            {
                timerToRest = timerToRest - Time.deltaTime;
            }
            if (timerToRest <= 0)
            {
                potRest = false;
            }
        } // Deja de colisionar con caldera

        // ---- Eventos de Colision con objetos que poseen RB ---- //

        Rigidbody body = hit.collider.attachedRigidbody; // Guarda en una variable de tipo rigidBody el objeto contra el que colisiona y busca que tenga rigidBody

        if (body == null || body.isKinematic) // No afecta a objetos que no tengan rigidbody o la propiedad isKinematic
            return;

        if (hit.moveDirection.y < -0.3f) // No empujar objetos por debajo nuestro
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z); // Calcula direccion de empuje. Nunca para arriba o abajo

        if (hit.gameObject.layer == 6)
        {
            body.velocity = pushDir * pushPower * pc.speed; // Aplica la fuerza de empuje
        } // Colision con los objetos pequeños. El 7 es para los muebles.
    }
}