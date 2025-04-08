using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieneClip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.objetos[1])
            {
                AudioManager.instance.Play("Clip");
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("No tienes clip");
            }
        }
    }
}
