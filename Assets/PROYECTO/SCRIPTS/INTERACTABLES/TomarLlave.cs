using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarLlave : MonoBehaviour
{
    [SerializeField]
    private GameObject llave;

    private void Update()
    {
        if (GameManager.instance.objetos[4])
        {
            llave.GetComponent<PickUp>().enabled = true;
        }
        else
        {
            Debug.Log("No tienes guantes");
        }
    }
}
