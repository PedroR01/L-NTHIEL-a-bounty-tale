using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Event_Manager : MonoBehaviour
{
    public static Event_Manager eventInstance;

    private void Awake()
    {
        eventInstance = this;
    }

    private void Start()
    {
    }

    public event Action<int, bool> onDoorTriggerEnter;

    public UnityEvent onPasiveMode;

    //static event Action onDoorTriggerEnter; // Para que se accesible sin inicializarlo

    #region ------  C# Event ------

    public void DoorTriggerEnter(int id, bool key)
    {
        onDoorTriggerEnter?.Invoke(id, key);
    }

    #endregion ------  C# Event ------

    #region ------  Unity Event ------

    public void HideUi()
    {
        onPasiveMode?.Invoke();
    }

    #endregion ------  Unity Event ------
}