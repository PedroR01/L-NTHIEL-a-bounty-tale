using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Audio : MonoBehaviour
{
    private Enemy enemy;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    private AudioClip clip;

    private float timer;
    private float timeToPlay;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        audioSource = GetComponent<AudioSource>();

        timer = 0.3f;
        timeToPlay = timer;
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
            PlayRespectiveSound();
    }

    private void PlayRespectiveSound()
    {
        if (enemy.stand)
        {
            timer = Random.Range(10, 15);
            clip = GetRandomClip(0, 1);
            StandSounds();
            //StartCoroutine(StandTiming()); // Bug con sonido. ¿?
        }
        if (enemy.patrol || enemy.chase)
        {
            timer = 0.3f;
            clip = GetRandomClip(2, 5);
            Footsteps();

            //StartCoroutine(FootstepsTiming(0.2f)); // Bug con sonido. ¿?
        }

        if (enemy.attack)
        {
            clip = GetRandomClip(6, 8);
            AttackingSounds(enemy.CanAttack());
        }
    }

    private void StandSounds()
    {
        timeToPlay -= Time.deltaTime;
        if (timeToPlay <= 0)
        {
            audioSource.volume = Random.Range(0.5f, 0.7f);
            audioSource.pitch = Random.Range(1, 1.5f);
            audioSource.PlayOneShot(clip);
            timeToPlay = timer;
        }
    }

    private void Footsteps()
    {
        timeToPlay -= Time.deltaTime;
        if (timeToPlay <= 0)
        {
            audioSource.volume = Random.Range(0.3f, 0.5f);
            audioSource.pitch = Random.Range(1, 1.5f);
            audioSource.PlayOneShot(clip);
            timeToPlay = timer;
        }
    }

    private void AttackingSounds(bool canPlay)
    {
        if (!canPlay)
            return;
        else if (canPlay)
        {
            audioSource.volume = Random.Range(0.5f, 0.8f);
            audioSource.pitch = 1;
            audioSource.PlayOneShot(clip);
        }
    }

    private AudioClip GetRandomClip(int startRange, int endRange)
    {
        return clips[Random.Range(startRange, endRange)];
    }

    /* private IEnumerator StandTiming()
    {
        yield return new WaitForSeconds(Random.Range(5, 7));
        clip = GetRandomClip(0, 1);
        audioSource.PlayOneShot(clip);
    }*/

    /* private IEnumerator FootstepsTiming(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.volume = Random.Range(0.3f, 0.5f);
        audioSource.pitch = Random.Range(1, 1.5f);
        audioSource.PlayOneShot(clip);
    }*/
}