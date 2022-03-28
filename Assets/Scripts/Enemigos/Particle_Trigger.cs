using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Trigger : MonoBehaviour
{
    private ParticleSystem blood;
    private bool activated;

    private void Start()
    {
        blood = GetComponentInChildren<ParticleSystem>();
        activated = false;
        EnableParticles();
    }

    private void Update()
    {
        EnableParticles();
    }

    private void EnableParticles()
    {
        var activation = blood.emission;

        if (activated)
        {
            blood.gameObject.SetActive(true);
            activation.enabled = true;
        }
        else
        {
            blood.gameObject.SetActive(false);
            activation.enabled = false;
        }
    }

    private IEnumerator DisableParticles()
    {
        yield return new WaitForSeconds(1f);
        activated = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            activated = true;
            StartCoroutine(DisableParticles());
        }
    }
}