using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private Player_Stats stats;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        stats = GetComponent<Player_Stats>();
    }

    private void Update()
    {
        Animations();
    }

    private void Animations()
    {
        if (Input.GetMouseButton(1))
        {
            anim.SetBool("aim", true);
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetTrigger("fire");
            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("aim", false);
        } // Animacion de apuntado y disparo.

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            anim.SetInteger("mov_Values", 0);
            anim.SetBool("walking", false);
            anim.SetBool("running", false);
            anim.SetBool("crouch", false);
            anim.SetTrigger("dash");
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            anim.SetInteger("mov_Values", 0);
            anim.SetBool("walking", false);
            anim.SetBool("running", false);
            anim.SetTrigger("jump");
        }

        if (Input.GetKey(KeyCode.LeftShift) && stats.canUseStamina || Input.GetKey(KeyCode.RightShift) && stats.canUseStamina)
        {
            anim.SetBool("walking", false);
            anim.SetBool("running", true);
            if (Input.GetKey(KeyCode.W))
                anim.SetInteger("mov_Values", 10);
            else if (Input.GetKey(KeyCode.S))
                anim.SetInteger("mov_Values", -10);
            else if (Input.GetKey(KeyCode.A))
                anim.SetInteger("mov_Values", -20);
            else if (Input.GetKey(KeyCode.D))
                anim.SetInteger("mov_Values", 20);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetInteger("mov_Values", 0);
            anim.SetBool("walking", false);
            anim.SetBool("crouch", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.SetBool("crouch", false);
            anim.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("mov_Values", 1);
            anim.SetBool("running", false);
            anim.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetInteger("mov_Values", -1);
            anim.SetBool("running", false);
            anim.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("mov_Values", -2);
            anim.SetBool("running", false);
            anim.SetBool("walking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("mov_Values", 2);
            anim.SetBool("running", false);
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetInteger("mov_Values", 0);
            anim.SetBool("walking", false);
            anim.SetBool("running", false);
            anim.SetBool("crouch", false);
        }
    }
}