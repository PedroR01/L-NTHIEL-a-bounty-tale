using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float reloadTime;

    [SerializeField] private Arrow arrowPrefab;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform releasePoint;

    public float munition;
    public float lastMunitionCheck;

    [SerializeField] private Weapon_Controller munitionController;

    private Arrow currentArrow;
    private bool isReloading;

    public void Reload()
    {
        if (isReloading && currentArrow != null || munition == 0) return; // Si hay algun bug con la aparicion de flechas, este condicional puede ser el problema.
        isReloading = true;
        lastMunitionCheck = munition;
        Debug.Log("arrowUsed == " + munitionController.arrowUsed);
        if (munitionController.arrowUsed)
        {
            StartCoroutine(ReloadAfterTime());
        }
        else if (!munitionController.arrowUsed)
        {
            currentArrow.transform.SetParent(spawnPoint);
            currentArrow.transform.localPosition = spawnPoint.transform.localPosition;
            currentArrow.gameObject.SetActive(true);
        }
    }

    private IEnumerator ReloadAfterTime()
    {
        yield return new WaitForSeconds(reloadTime);
        currentArrow = Instantiate(arrowPrefab, spawnPoint);
        currentArrow.transform.localPosition = Vector3.zero; // Ver donde poner esto si se spawnea mal la flecha
        isReloading = false;
    }

    public void ArrowInBow()
    {
        currentArrow.transform.SetParent(releasePoint);
        currentArrow.transform.localPosition = releasePoint.transform.localPosition - new Vector3(-0.2f, 1.9f, -1.2f);
        currentArrow.transform.localEulerAngles = Vector3.zero;
    } // Cambia el posicionamiento de la flecha a la ubicacion del arco

    public void CancelFire()
    {
        Debug.Log("La flecha " + currentArrow + "ha sido guardada");
        currentArrow.gameObject.SetActive(false);
        isReloading = false;
    }

    public void Fire(float firePower)
    {
        if (isReloading || currentArrow == null || munition == 0) return; // Ver estos condicionales en caso de que no funcione
        var force = releasePoint.TransformDirection(Vector3.forward * firePower);
        //Arrow theArrow = currentArrow.GetComponent<Arrow>();
        currentArrow.Fly(force);
        munition--;
        //currentArrow = null;
        //Reload();
    }

    public bool IsReady()
    {
        return (!isReloading && currentArrow != null && munition != 0);
    }
}