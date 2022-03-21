using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Starting_Cinematic : MonoBehaviour
{
    [SerializeField] private Start_Button starting;
    private Animator anim;
    private bool walk;

    private AudioSource aS;
    [SerializeField] private AudioClip[] clips;
    private AudioClip clip;
    private GameObject musicSound;

    private float timer;
    private float timeToPlay;

    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetInteger("mov_Values", 0);
        anim.SetBool("walking", false);
        anim.SetBool("running", false);
        anim.SetBool("crouch", false);
        walk = false;

        aS = GetComponent<AudioSource>();
        timer = 0.65f;
        timeToPlay = timer;

        musicSound = GameObject.Find("Dungeon");
    }

    private void Update()
    {
        if (!starting.start_Cinematic)
            return;
        else
        {
            walk = true;
            Animations();
            Movement();

            CheckToPlayFootsteps();
            AudioSource source = musicSound.GetComponent<AudioSource>();
            if (source.isPlaying)
                source.Stop();
        }
    }

    private void Animations()
    {
        if (walk) // Hacer temporizador para activar y desactivar walk.
        {
            anim.SetInteger("mov_Values", 1);
            anim.SetBool("running", false);
            anim.SetBool("walking", true);
        }
    }

    private void Movement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime, Space.World);
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
            aS.volume = Random.Range(0.2f, 0.4f);
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