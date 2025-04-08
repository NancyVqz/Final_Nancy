using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiEnemy : MonoBehaviour
{
    private Transform thePlayer;
    private NavMeshAgent agent;

    [SerializeField]
    private float radio;

    [SerializeField]
    private LayerMask playerMask;

    private Vector3 originPoint;
    private Vector3 charRotation;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
        originPoint = transform.position;
        charRotation = transform.localRotation.eulerAngles;
    }
    private void Update()
    {
        if (Physics.CheckSphere(transform.position, radio, playerMask))
        {
            agent.SetDestination(thePlayer.position);
            agent.stoppingDistance = 0;
        }
        else
        {
            agent.SetDestination(originPoint);
            agent.stoppingDistance = 0;
        }

        if (transform.position == originPoint)
        {
            transform.localRotation = Quaternion.Euler(charRotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radio);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bala")
        {
            Destroy(other.gameObject);
        }
    }
}
