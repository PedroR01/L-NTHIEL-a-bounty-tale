using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animator : MonoBehaviour
{
    private Animator anim;
    private Enemy behav;

    private void Start()
    {
        anim = GetComponent<Animator>();
        behav = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (behav.stand)
        {
            anim.SetBool("patrol", false);
            anim.SetBool("chasing", false);
            anim.SetBool("fighting", false);
        }
        else if (behav.patrol)
        {
            anim.SetBool("patrol", true);
            anim.SetBool("chasing", false);
            anim.SetBool("fighting", false);
        }
        else if (behav.chase)
        {
            anim.SetBool("patrol", false);
            anim.SetBool("chasing", true);
            anim.SetBool("fighting", false);
        }
        else if (behav.attack)
        {
            anim.SetBool("patrol", false);
            anim.SetBool("chasing", false);
            anim.SetBool("fighting", true);
            anim.SetTrigger("attack");
            //anim.ResetTrigger("attack");
        }
    }
}