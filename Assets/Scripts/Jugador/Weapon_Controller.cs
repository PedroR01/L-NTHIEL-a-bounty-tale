using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Controller : MonoBehaviour
{
    [SerializeField] private Bow bow;
    private Slider firePowerSlider;
    [SerializeField] private float maxFirePower;
    [SerializeField] private float firePower;
    [SerializeField] private float firePowerSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;
    [SerializeField] private float mouseY;
    [SerializeField] private LayerMask enemiesLayers;

    public Transform attackPointM;
    public float attackRangeM = 0.5f;
    private float damageM;

    [HideInInspector] public bool arrowUsed;
    [HideInInspector] public bool aiming;
    private bool charging;

    private void Start()
    {
        firePowerSlider = FindObjectOfType<Slider>();
        damageM = 10f;
        aiming = false;
        //bow.Reload();
    }

    private void Update()
    {
        MeleAttack();
        BowFire();
    }

    private void MeleAttack()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Collider[] golpearEnemigos = Physics.OverlapSphere(attackPointM.position, attackRangeM, enemiesLayers);

            foreach (Collider enemy in golpearEnemigos)
            {
                if (enemy == null)
                    return;
                else
                {
                    enemy.GetComponent<Enemy>().DamageReceived(damageM);
                    Debug.Log("Le pegaste a " + enemy.name);
                }
            }
        } // Ataque a mele
    }

    private void BowFire()
    {
        mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, minRotation, maxRotation);
        bow.transform.localRotation = Quaternion.Euler(mouseY, bow.transform.localEulerAngles.y, bow.transform.localEulerAngles.z);

        if (bow.munition == 0)
            return;
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                aiming = true;
                arrowUsed = false;
                bow.Reload();
                StartCoroutine(RepositioningArrow());
            }

            if (arrowUsed && Input.GetMouseButtonUp(1))
            {
                bow.DeleteArrow();
                aiming = false;
            }
            else if (!charging && Input.GetMouseButtonUp(1))
            {
                arrowUsed = false;
                aiming = false;
                bow.Reload();
            }

            if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
            {
                charging = true;
            }
            else if (!charging && aiming && arrowUsed && Input.GetMouseButton(1))
            {
                bow.Reload();
                StartCoroutine(RepositioningArrow());
                aiming = true;
                arrowUsed = false;
            }

            if (charging && firePower < maxFirePower)
            {
                firePower += Time.deltaTime * firePowerSpeed;
            }
            if (charging && Input.GetMouseButtonUp(0))
            {
                bow.Fire(firePower);
                firePower = 0;
                arrowUsed = true;
                charging = false;
            }

            if (charging)
                firePowerSlider.value = firePower;
        }
    }

    private IEnumerator RepositioningArrow()
    {
        yield return new WaitForSeconds(bow.reloadTime);
        bow.ArrowInBow();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointM == null) return;

        Gizmos.DrawSphere(attackPointM.position, attackRangeM);
    } // Para ver el punto de colision del ataque mele
}