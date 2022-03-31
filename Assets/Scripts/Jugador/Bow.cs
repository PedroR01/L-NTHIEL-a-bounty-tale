using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float reloadTime;

    [SerializeField] private Arrow arrowPrefab;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform releasePoint;

    public int munition;
    public float lastMunitionCheck;

    [SerializeField] private Weapon_Controller munitionController;

    private Arrow[] arrayArrow;
    private int arrowIndex;
    private Arrow currentArrow;
    [HideInInspector] public bool isReloading;

    private void Start()
    {
        arrayArrow = new Arrow[munition];
        for (int i = 0; i < arrayArrow.Length; i++)
        {
            arrayArrow[i] = Instantiate(arrowPrefab, spawnPoint);
            arrayArrow[i].gameObject.SetActive(false);
        }

        arrowIndex = -1;
    }

    public void Reload()
    {
        if (isReloading && arrayArrow[arrowIndex] != null || munition == 0) return;
        isReloading = true;
        lastMunitionCheck = munition;

        // currentArrow = arrayArrow[arrowIndex];

        if (munitionController.arrowUsed || arrowIndex == -1)
        {
            StartCoroutine(ReloadAfterTime());
        }
        else if (munitionController.aiming)
        {
            StartCoroutine(Reactivate());
        }
        else if (!munitionController.aiming && !munitionController.arrowUsed)
        {
            CancelFire();
        }
    }

    private IEnumerator ReloadAfterTime()
    {
        yield return new WaitForSeconds(reloadTime);
        if (arrowIndex < arrayArrow.Length - 1)
        {
            arrowIndex++;
            Debug.Log("flecha numero: " + arrowIndex);
        }
        else
            arrowIndex = 0;

        arrayArrow[arrowIndex].gameObject.SetActive(true);
        arrayArrow[arrowIndex].transform.localPosition = Vector3.zero;
        isReloading = false;
    }

    public void ArrowInBow()
    {
        arrayArrow[arrowIndex].transform.SetParent(releasePoint);
        arrayArrow[arrowIndex].transform.localPosition = releasePoint.transform.localPosition - new Vector3(-0.2f, 1.9f, -1.2f);
        arrayArrow[arrowIndex].transform.localEulerAngles = Vector3.zero;
    } // Cambia el posicionamiento de la flecha a la ubicacion del arco

    public void CancelFire()
    {
        Debug.Log("La flecha " + arrayArrow[arrowIndex] + "ha sido guardada");
        arrayArrow[arrowIndex].gameObject.SetActive(false);
        isReloading = false;
    }

    public void Fire(float firePower)
    {
        if (isReloading || arrayArrow[arrowIndex] == null || munition == 0) return;
        var force = releasePoint.TransformDirection(Vector3.forward * firePower);
        //Arrow theArrow = currentArrow.GetComponent<Arrow>();
        arrayArrow[arrowIndex].Fly(force);
        munition--;

        //currentArrow = null;
        //Reload();
    }

    public bool IsReady()
    {
        return (!isReloading && arrayArrow[arrowIndex] != null && munition != 0);
    }

    public void DeleteArrow()
    {
        Debug.Log("Flecha borrada");
        // arrayArrow[arrowIndex].gameObject.SetActive(false);
    }

    private IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(reloadTime);
        arrayArrow[arrowIndex].gameObject.SetActive(true);
        isReloading = false;
    }
}