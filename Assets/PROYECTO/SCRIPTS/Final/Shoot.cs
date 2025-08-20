using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private int range;
    [SerializeField] private LayerMask layerTarget;
    [SerializeField] private LayerMask layerExtra;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * range, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.Play("Shoot");
            MouseEnter();
        }
    }

    private void MouseEnter()
    {
        ObjectiveSpawn objectiveSpawn = FindObjectOfType<ObjectiveSpawn>();
        ContadorTime contadorTime = FindObjectOfType<ContadorTime>();
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range, layerTarget))
        {
            objectiveSpawn.OnObjectiveClicked(hit.collider.gameObject);
        } 
        else if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range, layerExtra))
        {
            contadorTime.ExtraTime();
            Destroy(hit.collider.gameObject);
        }


    }
}
