using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stamina : MonoBehaviour
{
    [SerializeField] private Player_Stats stamina;
    private Image staminaBar;

    private void Start()
    {
        staminaBar = this.gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (stamina.lastStaminaValue != stamina.actualStamina)
            UpdateStamina();
    }

    private void UpdateStamina()
    {
        float normalizedStamina = stamina.actualStamina / stamina.maxStamina;
        staminaBar.fillAmount = normalizedStamina;
    }
}