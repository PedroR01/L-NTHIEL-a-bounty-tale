using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Door : MonoBehaviour
{
    public int id;
    public bool key;
    public bool opening;

    private void Start()
    {
        opening = false;
        OnDoorWayOpen(id, key);
    }

    private void Subscribe()
    {
        Event_Manager.eventInstance.onDoorTriggerEnter += OnDoorWayOpen;
        //Event_Manager.onDoorTriggerEnter += OnDoorWayOpen; // Si el evento fuera static en lugar de public
    }

    private void Unsubscribe()
    {
        Event_Manager.eventInstance.onDoorTriggerEnter -= OnDoorWayOpen;
        Debug.Log("Unsubscribed");
    }

    private void OnDoorWayOpen(int _id, bool _key)
    {
        if (_id == id && _key == key)
        {
            Subscribe();
            if (opening)
                OpenningDoor();
        }
    }

    private void OpenningDoor()
    {
        float rotationVelocity = 0.2f;
        Quaternion rotationValue = Quaternion.Euler(new Vector3(0f, 130f, 0f));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationValue, rotationVelocity);
        if (transform.rotation.y == 130f)
        {
            opening = false;
            Unsubscribe();
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}