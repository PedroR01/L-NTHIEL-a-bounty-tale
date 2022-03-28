using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Post_Processing_Changes : MonoBehaviour
{
    [SerializeField] private Canvas uiMessage;
    [SerializeField] private Player_Controller playerLook;
    [SerializeField] private GameObject[] dreamObjects; // Ver si comentar esto
    [SerializeField] private PostProcessProfile profile; // Guardo el otro perfil por si me interesa cambiarlo bajo un evento
    private PostProcessVolume volume;
    private ChromaticAberration aberration;
    private float values;

    private bool wakeUp;

    private void Start()
    {
        //playerLook = FindObjectOfType<Player_Controller>();
        wakeUp = false;
        volume = Camera.main.GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            wakeUp = true;

        if (wakeUp)
            WakeUpFromDream();
    }

    private void WakeUpFromDream()
    {
        if (volume.profile != profile)
        {
            /* if (playerLook.smoothing > 0)
             {
                 Debug.Log("You're starting to wake up");
                 playerLook.smoothing = 0f;
             }*/

            volume.profile.TryGetSettings(out aberration);

            if (!aberration.active)
                return;

            if (aberration.active)
            {
                values = aberration.intensity - 0.2f * Time.deltaTime;
                aberration.intensity.Override(values);
            }

            if (aberration.intensity <= 0 && aberration.active)
            {
                Debug.Log("You're awake from Freddy´s dream");
                for (int i = 0; i < dreamObjects.Length; i++)
                {
                    dreamObjects[i].SetActive(false);
                }

                aberration.active = false;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == 6 && wakeUp) // Invisible wall
            Destroy(hit.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && !wakeUp) // Trigger wall
            uiMessage.gameObject.SetActive(true);

        /* if (other.name == "Trigger Pp Profile Change")
             volume.profile = profile;*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7 && !wakeUp) // Trigger wall
            uiMessage.gameObject.SetActive(false);
    }
}