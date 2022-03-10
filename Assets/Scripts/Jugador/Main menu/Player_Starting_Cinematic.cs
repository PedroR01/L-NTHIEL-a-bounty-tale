using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Starting_Cinematic : MonoBehaviour
{
    [SerializeField] private Start_Button starting;
    private Animator anim;
    private bool walk;

    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetInteger("mov_Values", 0);
        anim.SetBool("walking", false);
        anim.SetBool("running", false);
        anim.SetBool("crouch", false);
        walk = false;
    }

    private void Update()
    {
        if (starting.start_Cinematic)
        {
            walk = true;
            Animations();
            Movement();
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
        if (transform.position.z >= 8f)
        {
            SceneManager.LoadScene("Dungeon_Level1");
        }
    }
}