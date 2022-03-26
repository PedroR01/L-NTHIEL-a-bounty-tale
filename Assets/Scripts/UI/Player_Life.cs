using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Life : MonoBehaviour
{
    private Player_Stats life;
    private Image lifeBar;

    private void Start()
    {
        life = FindObjectOfType<Player_Stats>();
        lifeBar = this.gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (life == null)
            life = FindObjectOfType<Player_Stats>();

        if (life.lastLifeValue != life.actualLife)
            UpdateLife();
    }

    private void UpdateLife()
    {
        float normalizedLife = life.actualLife / life.maxLife;
        lifeBar.fillAmount = normalizedLife;
    }
}