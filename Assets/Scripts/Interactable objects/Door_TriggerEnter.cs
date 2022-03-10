using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_TriggerEnter : MonoBehaviour
{
    private Interactable_Door doorVar;

    private void Start()
    {
        doorVar = GetComponent<Interactable_Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        doorVar.opening = true;
        Event_Manager.eventInstance.DoorTriggerEnter(doorVar.id, doorVar.key);
    }
}