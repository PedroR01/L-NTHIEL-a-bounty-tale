using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Aiming_Camera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera lookCamera;
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private GameObject aimReticle;

    //Cinemachine.CinemachineImpulseSource source;

    private void Update()
    {
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
            aimReticle.SetActive(false);
        }
    }

    private IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }
}