using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio : MonoBehaviour
{
    private CharacterController cc;
    private AudioSource audioSource;
    [SerializeField] private Bow reloadTimer;
    [SerializeField] private AudioClip[] clips;
    private AudioClip clip;

    private float acumulatedDistance;
    [HideInInspector] public float stepDistance;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!cc.isGrounded)
            return;

        if (cc.isGrounded && !audioSource.isPlaying && cc.velocity.sqrMagnitude > 0)
        {
            clip = GetRandomClip(0, 3);
            Play();
        }

        if (Input.GetMouseButtonDown(1))
        {
            clip = clips[4];
            audioSource.PlayOneShot(clip);
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && !reloadTimer.isReloading)
        {
            clip = GetRandomClip(5, 6);
            audioSource.PlayOneShot(clip);
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButtonUp(0) && !reloadTimer.isReloading)
        {
            clip = GetRandomClip(7, 8);
            audioSource.PlayOneShot(clip);
        }
    }

    private void Play()
    {
        acumulatedDistance += Time.deltaTime;
        if (acumulatedDistance > stepDistance)
        {
            audioSource.volume = Random.Range(0.3f, 0.5f);
            audioSource.pitch = Random.Range(1, 1.5f);
            //audioSource.Play();
            audioSource.PlayOneShot(clip);
            acumulatedDistance = 0;
        }
    }

    private AudioClip GetRandomClip(int startRange, int endRange)
    {
        return clips[Random.Range(startRange, endRange)];
    }
}