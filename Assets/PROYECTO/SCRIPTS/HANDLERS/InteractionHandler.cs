using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public float detectionRange;
    public LayerMask detectionLayers;
    public Ray ray;
    RaycastHit hit;

    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, detectionRange, detectionLayers)) // Esta revisando si esta chocando con una de las layers o no
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.GetComponent<IInteractable>().Interact();
            }
        }

    }
}
