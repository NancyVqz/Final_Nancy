using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerLlave : MonoBehaviour
{
    [SerializeField]
    private GameObject lentes;

    void Update()
    {
        if (GameManager.instance.objetos[7])
        {
            lentes.SetActive(true);
            AudioManager.instance.Play("Lentes");
        }
    }
}
