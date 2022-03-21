using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio : MonoBehaviour
{
    [SerializeField] private Start_Button starting;
    private AudioSource aS;
    [SerializeField] private AudioClip[] clips;
    private AudioClip clip;

    private float timer;
    private float timeToPlay;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
        timer = 0.75f;
        timeToPlay = timer;
    }

    private void Update()
    {
        if (!starting.start_Cinematic)
            return;
        else
            CheckToPlayFootsteps();
    }

    private void Steps(AudioClip clip)
    {
        aS.PlayOneShot(clip);
    }

    private void CheckToPlayFootsteps()
    {
        clip = GetRandomClip();
        timeToPlay -= Time.deltaTime;
        if (timeToPlay <= 0)
        {
            aS.volume = Random.Range(0.3f, 0.5f);
            aS.pitch = Random.Range(1, 1.5f);
            Steps(clip);
            timeToPlay = timer;
        }
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}