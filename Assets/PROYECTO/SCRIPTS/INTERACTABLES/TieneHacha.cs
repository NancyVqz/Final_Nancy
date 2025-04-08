using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieneHacha : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.objetos[0])
            {
                AudioManager.instance.Play("Hacha");
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("No tienes hacha");
            }
        }
    }
}
