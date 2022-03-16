using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    private PlayerController pc;
    [SerializeField] private Game_Manager gm;

    // Basic stats variables

    public float maxLife;
    public float lastLifeValue;
    public float actualLife;
    public float maxStamina;
    public float lastStaminaValue;
    public float actualStamina;
    private float damageM;
    private float armour;

    // Skill tree stats

    private float agility;
    private float bonusSpeed;

    // Basic timer variables

    public bool timeWithoutDamage;
    public bool timeRecovering;
    public bool canUseStamina;
    private float actualTimeLife;
    private float lifeTimer;
    private float actualTimeStamina;
    private float staminaTimer;

    [HideInInspector] public bool dead;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        gm.SetSkillTree(Game_Manager.mainSkillTree.Agility);
        SkillTreeStats();
        BasicStats();
        dead = false;
    }

    private void Update()
    {
        if (dead)
            return;

        if (actualLife <= 0)
            Muerto();

        if (actualStamina < maxStamina)
            StartCoroutine(UpdateStamina());

        if (actualLife < maxLife)
            StartCoroutine(UpdateLife());

        Heal();
        StaminaRecovery();
    }

    private void BasicStats()
    {
        maxLife = 100f;
        maxStamina = 90f + agility * 2;
        damageM = 15f;
        armour = 10f;

        actualLife = maxLife;
        lastLifeValue = actualLife;
        actualStamina = maxStamina;
        lastStaminaValue = actualStamina;

        // Temporizadores
        timeRecovering = false;
        canUseStamina = true;
        lifeTimer = 5;
        actualTimeLife = lifeTimer;
        staminaTimer = 5;
        actualTimeStamina = staminaTimer;
        // ------------

        if (gm.GetSkillTree() != Game_Manager.mainSkillTree.Agility)
        {
            agility = 0;
        }
    }

    private void SkillTreeStats()
    {
        switch (gm.GetSkillTree())
        {
            case Game_Manager.mainSkillTree.Agility:
                Debug.Log("You have chosen the Agility path. Running and climbing trying to escape during your childhood forged your main attributes");
                AgilityTree();
                break;

            case Game_Manager.mainSkillTree.Stealth:
                Debug.Log("You have chosen the Stealth path. Born in darkness, you live and remain as a shadow");
                break;

            case Game_Manager.mainSkillTree.Focus:
                Debug.Log("You have chosen the Focus path. Always thinking before act, you develop a feeling like everything moves slower when you are extremely focus");
                break;
        }
    }

    private void AgilityTree()
    {
        agility = 10f; // Reduce arrow coldown to fire and increase max stamina
        bonusSpeed = 0.5f; // More base speed and speed limit
    }

    public void GetAgilityStats(float agility_, out float _agility)
    {
        _agility = agility;
    }

    public void GetAgilityStats(out float _bonusSpeed, float speed)
    {
        _bonusSpeed = bonusSpeed;
    }

    public float GetDanio()
    {
        return damageM;
    }

    public void RecibirDanio(float danioRecibido)
    {
        actualLife -= danioRecibido - armour / 2;

        // Animacion de recibir danio
    }

    private void Heal()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //actualLife -= 10;
            actualLife = 0f;
        }

        if (actualLife < maxLife)
            StartCoroutine(TimeWithoutDamage(lastLifeValue));
        if (actualLife < maxLife && timeWithoutDamage)
        {
            actualLife += 5f * Time.deltaTime;
            Debug.Log("Realizando curacion. Vida actual: " + actualLife);
        }
    } // Ajustar el temporizador. Que tarde mas tiempo. Con la corrutina no se me actualiza la info de la vida en el HUD de forma correcta, nose por que

    private IEnumerator TimeWithoutDamage(float _lastLifeValue)
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Corrutina 1");
        if (_lastLifeValue == actualLife)
            timeWithoutDamage = true;
        else if (_lastLifeValue != actualLife)
        {
            timeWithoutDamage = false;
            lastLifeValue = actualLife;
        }
    }

    private void StaminaRecovery()
    {
        if (actualStamina <= 0f)
            canUseStamina = false;
        if (canUseStamina == false && actualStamina >= 15f)
            canUseStamina = true;

        if (lastStaminaValue != actualStamina && !timeRecovering || !timeRecovering)
            actualTimeStamina = staminaTimer;

        if (actualStamina >= maxStamina)
            timeRecovering = false;
        else if (actualStamina < maxStamina && timeRecovering)
        {
            actualTimeStamina -= Time.deltaTime;
            if (actualTimeStamina <= 0)
            {
                actualStamina += 5f * (Time.deltaTime / 2);
                Debug.Log("Recuperando energia. Energia actual: " + actualStamina);
            }
        }
        else if (lastStaminaValue == actualStamina)
        {
            timeRecovering = true;
        }
    } // No pude solucionar de manera interna para que cada vez que corra, el temporizador vuelva a su valor original, por lo que lo resolvi momentaneamente en el script PlayerController

    #region Coroutines

    private IEnumerator UpdateStamina()
    {
        yield return new WaitForSeconds(0.2f);
        lastStaminaValue = actualStamina; // Estos valores se actualizan luego de un minimo de tiempo
    }

    private IEnumerator UpdateLife()
    {
        yield return new WaitForSeconds(1f);
        // Debug.Log("Corrutina 2");
        lastLifeValue = actualLife; // Tuve que hacer 2 corrutinas porque sino al mezclar los distintos metodos de perdida y recuperacion, me tiraba errores.
                                    // Tal vez haya una solucion mas optima que esta
    }

    #endregion Coroutines

    private void Muerto()
    {
        Debug.Log("Moriste... Fin.");
        dead = true;
        //this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
}