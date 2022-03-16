using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio_Alternative : MonoBehaviour
{
    private CharacterController cc;
    private AudioSource audioSource;
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
            clip = GetRandomClip();
            Play();
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

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}