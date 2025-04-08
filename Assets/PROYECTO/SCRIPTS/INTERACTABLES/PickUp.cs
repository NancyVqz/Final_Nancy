using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    public Item item;

    private InventoryHandler inventory;

    public int id;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryHandler>();
    }

    public void Interact()
    {
        inventory.AgregarObjeto(item);
        Debug.Log(item.name + " Añadida al inventario");
        Debug.Log("Descripcion: " + item._description);
        GameManager.instance.objetos[id] = true;
        Destroy(gameObject);
    }
}
