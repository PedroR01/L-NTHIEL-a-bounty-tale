using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected float radius = 2.5f; // Distancia para que el player interactua
    [SerializeField] protected Transform interactionTransform;
    [SerializeField] private Transform player;

    public bool canInteract = false;

    protected virtual void Interact()
    {
        Debug.Log("You can pick this item: " + this.name);
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, interactionTransform.position);
        if (distance <= radius && !canInteract)
        {
            Interact();
            canInteract = true;
        }
        else if (distance > radius)
        {
            canInteract = false;
        }
    }

    private void OnDrawGizmosSelected() // Cambiar esto por un trigger collider.
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}