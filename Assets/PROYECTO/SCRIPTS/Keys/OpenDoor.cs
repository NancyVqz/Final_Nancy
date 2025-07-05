using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private int id;
    private Animator animDoor;
    private BoxCollider colliderTrigger;

    private void Start()
    {
        animDoor = GetComponentInChildren<Animator>();
        colliderTrigger = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!GameManager.instance.keyID[id])
            {
                Debug.Log("No tienes la llave");
                return;
            }
            else
            {
                AudioManager.instance.Play("Llave");
                animDoor.SetTrigger("Open");
                colliderTrigger.enabled = false;
            }
        }
    }
}
