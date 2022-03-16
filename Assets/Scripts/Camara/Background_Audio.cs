using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Audio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    private AudioClip clip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "Main Dungeon" && other.gameObject.layer == 10)
        {
            // buscar manera de dejar de reproducir el sonido de fondo de la prision
            clip = clips[0];
            audioSource.PlayOneShot(clip);
        }
        if (this.gameObject.name == "Prision Dungeon" && other.gameObject.layer == 10)
        {
            clip = clips[1];
            audioSource.PlayOneShot(clip);
        }
    }
}