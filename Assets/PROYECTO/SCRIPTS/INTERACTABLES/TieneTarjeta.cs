using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieneTarjeta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.objetos[3])
            {
                AudioManager.instance.Play("Tarjeta");
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("No tienes tarjeta");
            }
        }
    }
}
