using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.objetos[2])
            {
                AudioManager.instance.Play("Mascara");              
            }
            else
            {
                PlayerHealth.instance.Reaparecer();
            }
        }
    }
}
