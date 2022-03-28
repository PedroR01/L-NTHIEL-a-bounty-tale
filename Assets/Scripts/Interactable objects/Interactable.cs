using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected float radius = 2.5f; // Distancia para que el player interactua
    [SerializeField] protected Transform interactionTransform;
    [SerializeField] private Transform player;
    protected GameObject uiMessage;

    public bool canInteract = false;

    private void Update()
    {
        float distance = Vector3.Distance(player.position, interactionTransform.position);
        if (distance <= radius)
        {
            if (uiMessage == null)
                uiMessage = GameObject.Find("Instructions Panel");
            else
            {
                uiMessage.GetComponent<Image>().enabled = true;
                uiMessage.GetComponentInChildren<Text>().enabled = true;
            }

            //uiMessage = GameObject.Find("/HUD/Instructions Panel");

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
                canInteract = true;
            }
        }
        else if (distance > radius)
        {
            canInteract = false;
        }
    }

    protected virtual void Interact()
    {
    }

    protected virtual void OnDrawGizmosSelected() // Cambiar esto por un trigger collider.
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}