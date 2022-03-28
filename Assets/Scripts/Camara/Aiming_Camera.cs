using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Aiming_Camera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera lookCamera;
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private GameObject aimReticle;

    //Cinemachine.CinemachineImpulseSource source;

    private void Start()
    {
        if (aimReticle == null)
            aimReticle = GameObject.Find("Reticle");
    }

    private void Update()
    {
        if (aimReticle == null)
            aimReticle = GameObject.Find("Reticle");

        ChangeCamera();
    }

    private void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            aimCamera.gameObject.SetActive(true);
            lookCamera.gameObject.SetActive(false);
            StartCoroutine(ShowReticle());
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            lookCamera.gameObject.SetActive(true);
            aimCamera.gameObject.SetActive(false);
            //aimReticle.SetActive(false);
            aimReticle.GetComponent<Image>().enabled = false;
            aimReticle.GetComponentInChildren<Image>().enabled = false;
        }
    }

    private IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        //aimReticle.SetActive(enabled);
        aimReticle.GetComponent<Image>().enabled = true;
        aimReticle.GetComponentInChildren<Image>().enabled = true;
    }
}