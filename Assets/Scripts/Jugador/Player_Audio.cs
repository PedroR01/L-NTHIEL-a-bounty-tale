using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio : MonoBehaviour
{
    private CharacterController cc;
    private AudioSource aS;

    private float acumulatedDistance;
    [HideInInspector] public float stepDistance;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        aS = FindObjectOfType<Audio_Manager>().GetComponent<AudioSource>();
        ;
    }

    private void Update()
    {
        CheckToPlayFootsteps();
    }

    private void Steps(int number)
    {
        // if (cc.isGrounded && cc.velocity.magnitude > 2 && !aS.isPlaying)
        FindObjectOfType<Audio_Manager>().Play("Footsteps" + number.ToString());
    }

    private void CheckToPlayFootsteps()
    {
        if (!cc.isGrounded)
            return;

        if (cc.velocity.sqrMagnitude > 0)
        {
            acumulatedDistance += Time.deltaTime;
            if (acumulatedDistance > stepDistance)
            {
                Steps(Random.Range(1, 4));
                acumulatedDistance = 0;
            }
        }
        else
        {
            acumulatedDistance = 0;
        }
    }
}