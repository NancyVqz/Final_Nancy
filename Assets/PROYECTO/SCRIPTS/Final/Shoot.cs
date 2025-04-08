using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private float velocidadBala = 970;

    private Transform puntoDisparo;

    private float machineCont = 0;

    [SerializeField]
    private GameObject bala;

    private void Start()
    {
        puntoDisparo = transform.parent;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && machineCont <= 0)
        {
            AudioManager.instance.Play("Shoot");
            DispararBala();
            machineCont = 1f;
        }
        machineCont -= Time.deltaTime;
    }

    private void DispararBala()
    {
        GameObject cosa = Instantiate(bala, puntoDisparo.position, bala.transform.rotation);
        cosa.GetComponent<Rigidbody>().AddForce(transform.forward * velocidadBala);
        Destroy(cosa, 2f);
    }
}
