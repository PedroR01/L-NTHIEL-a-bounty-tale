using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinemachine_Selection : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] v_Cameras;
    [SerializeField] private Transform player;
    [SerializeField] private Start_Button starting;
    private int cameraNum;

    private void Start()
    {
        cameraNum = 0;
    }

    private void Update()
    {
        ChangeCameras();
    }

    private void ChangeCameras()
    {
        if (v_Cameras[0].gameObject.activeInHierarchy == true && starting.start_Cinematic)
        {
            cameraNum++;
            v_Cameras[cameraNum].gameObject.SetActive(!v_Cameras[cameraNum].gameObject.activeInHierarchy);
            v_Cameras[0].gameObject.SetActive(!v_Cameras[cameraNum].gameObject.activeInHierarchy);
        }
        else if (v_Cameras[1].gameObject.activeInHierarchy == true && player.position.z >= 0f)
        {
            v_Cameras[cameraNum].gameObject.SetActive(!v_Cameras[cameraNum].gameObject.activeInHierarchy);
            cameraNum++;
            v_Cameras[cameraNum].gameObject.SetActive(!v_Cameras[cameraNum].gameObject.activeInHierarchy);
        }
        else if (v_Cameras[2].gameObject.activeInHierarchy == true && player.position.z >= 5f)
        {
            v_Cameras[cameraNum].gameObject.SetActive(!v_Cameras[cameraNum].gameObject.activeInHierarchy);
            cameraNum++;
            v_Cameras[cameraNum].gameObject.SetActive(!v_Cameras[cameraNum].gameObject.activeInHierarchy);
        }
    }
}