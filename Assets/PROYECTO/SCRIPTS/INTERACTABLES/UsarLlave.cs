using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsarLlave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.objetos[5])
            {
                AudioManager.instance.Play("Llave");
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("No tienes la llave maldita");
            }
        }
    }
}
