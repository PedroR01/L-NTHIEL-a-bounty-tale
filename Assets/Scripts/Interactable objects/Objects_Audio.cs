using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects_Audio : MonoBehaviour
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
        if (this.gameObject.tag == "Door" && !audioSource.isPlaying)
            DoorSound();

        if (this.gameObject.tag == "Torch" && !audioSource.isPlaying)
            TorchSound();

        if (this.gameObject.tag == "Props" && !audioSource.isPlaying)
            PropsSound();
    }

    private void DoorSound()
    {
        clip = clips[0];
        audioSource.pitch = Random.Range(1, 1.4f);
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
    }

    private void TorchSound()
    {
        clip = clips[1];
        audioSource.Play();
    }

    private void PropsSound()
    {
        clip = clips[2];
        audioSource.PlayOneShot(clip);
    }
}