using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Button : MonoBehaviour
{
    public bool start_Cinematic;

    private void Start()
    {
        start_Cinematic = false;
    }

    public void Click()
    {
        start_Cinematic = true;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("Canvas").gameObject.SetActive(false);
    }
}