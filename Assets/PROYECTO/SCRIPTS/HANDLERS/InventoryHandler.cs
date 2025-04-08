using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] private List<Item> inventario; // Esta lista es mi inventario y aqui se guardan game objects
    public List<Item> _Inventario { get => inventario; } // Esta variable me permite leer el inventario desde otros scripts, pero no modificarlo

    public int indice;
    public int maxCapacity = 8;

    public int numero;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TirarObjeto();
        }
    }

    public void AgregarObjeto(Item item)
    {
        inventario.Add(item);
    }

    public void TirarObjeto()
    {
        Instantiate(inventario[indice]._prefab, transform.position, transform.rotation);
        inventario.RemoveAt(indice);
    }
}
