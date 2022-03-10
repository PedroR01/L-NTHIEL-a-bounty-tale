using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Controller : MonoBehaviour
{
    [SerializeField] private Bow bow;
    [SerializeField] private Slider firePowerSlider; // Cambiar esto despues a un script aparte con todo lo de UI
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

    public bool arrowUsed;
    private bool aiming;
    private bool charging;

    private void Start()
    {
        arrowUsed = true;
        damageM = 10f;
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
                enemy.GetComponent<Enemy>().DamageReceived(damageM);
                Debug.Log("Le pegaste a " + enemy.name);
            }
        } // Ataque a mele
    }

    private void BowFire()
    {
        mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, minRotation, maxRotation);
        bow.transform.localRotation = Quaternion.Euler(mouseY, bow.transform.localEulerAngles.y, bow.transform.localEulerAngles.z);

        if (Input.GetMouseButtonDown(1))
        {
            bow.Reload();
            StartCoroutine(RepositioningArrow());
        }
        if (!charging && Input.GetMouseButtonUp(1))
        {
            arrowUsed = false;
            bow.CancelFire();
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0))
            charging = true;

        if (charging && firePower < maxFirePower)
        {
            firePower += Time.deltaTime * firePowerSpeed;
        }
        if (charging && Input.GetMouseButtonUp(0))
        {
            bow.Fire(firePower); // Esto deberia ir en un FixedUpdate?
            firePower = 0;
            arrowUsed = true;
            charging = false;
        }

        if (charging)
        {
            firePowerSlider.value = firePower;
            //firePowerSlider.value = 1;
        }
    }

    private IEnumerator RepositioningArrow()
    {
        yield return new WaitForSeconds(bow.reloadTime + 0.2f);
        bow.ArrowInBow();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointM == null) return;

        Gizmos.DrawSphere(attackPointM.position, attackRangeM);
    } // Para ver el punto de colision del ataque mele
}