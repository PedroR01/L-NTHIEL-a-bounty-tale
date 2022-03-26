using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stamina : MonoBehaviour
{
    private Player_Stats stamina;
    private Image staminaBar;

    private void Start()
    {
        stamina = FindObjectOfType<Player_Stats>();
        staminaBar = this.gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (stamina == null)
            stamina = FindObjectOfType<Player_Stats>();

        if (stamina.lastStaminaValue != stamina.actualStamina)
            UpdateStamina();
    }

    private void UpdateStamina()
    {
        float normalizedStamina = stamina.actualStamina / stamina.maxStamina;
        staminaBar.fillAmount = normalizedStamina;
    }
}