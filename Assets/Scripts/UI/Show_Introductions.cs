using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Introductions : MonoBehaviour
{
    private Canvas[] introduction = new Canvas[2];

    private void Start()
    {
        introduction[0] = FindObjectOfType<Canvas>();
        introduction[0].enabled = false;

        introduction[1] = this.gameObject.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (introduction[1].isActiveAndEnabled)
            StartCoroutine(IntroductionTime());
    }

    private IEnumerator IntroductionTime()
    {
        yield return new WaitForSeconds(7f);
        introduction[1].enabled = false;
        introduction[0].enabled = true;
    }
}