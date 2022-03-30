using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Tree_Powers : MonoBehaviour
{
    /// <summary>
    /// PODRIA HACER DE ESTO UNA CLASE PADRE Y LUEGO HEREDAR DEPENDIENDO DEL ARBOL DE HABILIDADES ELEGIDO
    /// </summary>
    private PlayerController pController;

    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    private void Start()
    {
        pController = GetComponent<PlayerController>();
    }

    public void DashAbility(int direction)
    {
        if (direction == 1)
            StartCoroutine(ForwardDash());
        else if (direction == -1)
            StartCoroutine(BackwardDash());
    }

    private IEnumerator ForwardDash()
    {
        float startTime = Time.time; // El tiempo a partir del comienzo del frame en el que se ejecuto esta linea
        while (Time.time < startTime + dashTime)
        {
            pController.characterController.Move(pController.gameObject.transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator BackwardDash()
    {
        float startTime = Time.time; // El tiempo a partir del comienzo del frame en el que se ejecuto esta linea
        while (Time.time < startTime + dashTime)
        {
            pController.characterController.Move(pController.gameObject.transform.forward * (dashSpeed * -1) * Time.deltaTime);
            yield return null;
        }
    }
}