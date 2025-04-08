using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float radio;

    [SerializeField]
    private LayerMask playerMask;

    private bool permiso = true;

    [SerializeField]
    private ParticleSystem explosionEffecto;

    private void Update()
    {
        if (permiso) 
        {
            if (Physics.CheckSphere(transform.position, radio, playerMask))
            {
                ExplosionEffect();
                Destroy(gameObject);

            }
            else
            {

            }
        }

    }

    private void ExplosionEffect()
    {
        PlayerHealth.instance.RestarVida();
        AudioManager.instance.Play("Explosion");
        GameObject copia = Instantiate(explosionEffecto.gameObject, transform.position, transform.rotation);
        permiso = false;
        Destroy(copia, 1f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
