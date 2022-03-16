using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Canvas : MonoBehaviour
{
    [SerializeField] private Player_Stats stats_Timer;

    private void Awake()
    {
        if (this != null)
            DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (stats_Timer.timeWithoutDamage) // Fijarse de añadir un booleano para que una vez que este suscripto no se vuelva a suscribir
            Subscribe();
        //Debug.Log("UI SUBSCRIBED");
        else if (!stats_Timer.timeWithoutDamage)
            Unsubscribe();
        //Debug.Log("UI UNSUBSCRIBED");
    }

    public void PasiveModeHandler()
    {
        Event_Manager.eventInstance.HideUi();
    }

    private void Subscribe()
    {
        Event_Manager.eventInstance.onPasiveMode.AddListener(PasiveModeHandler);
    }

    private void Unsubscribe()
    {
        Event_Manager.eventInstance.onPasiveMode.RemoveListener(PasiveModeHandler);
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}