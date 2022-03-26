using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Arrows_Munition : MonoBehaviour
{
    private Text munitionText;

    //[SerializeField] private Combat mun;
    private Bow mun;

    private void Start()
    {
        mun = FindObjectOfType<Bow>();
        munitionText = this.gameObject.GetComponent<Text>();
        UpdateMunition();
    }

    private void Update()
    {
        if (mun == null)
            mun = FindObjectOfType<Bow>();

        if (mun.lastMunitionCheck != mun.munition)
            UpdateMunition();
    }

    private void UpdateMunition()
    {
        munitionText.text = "X " + mun.munition;
    }

    private void ShowMunition()
    {
    } // Shows this part of the UI if the munition change
}