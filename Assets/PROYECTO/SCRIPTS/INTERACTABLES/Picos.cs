using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.objetos[6])
            {
                AudioManager.instance.Play("Shoe");
            }
            else
            {
                PlayerHealth.instance.Reaparecer();
            }
        }
    }
}
