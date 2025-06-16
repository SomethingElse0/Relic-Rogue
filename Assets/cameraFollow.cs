using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.speed = 5;
    }
    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(new Vector3(player.position.x, player.position.y, transform.position.z+0.2f*Mathf.Sin(Time.fixedTime)));
        if (agent.remainingDistance > 1 && agent.velocity.magnitude == 0) agent.SetDestination(new Vector3(player.position.x + 0.1f, player.position.y, transform.position.z + 0.2f * Mathf.Sin(Time.fixedTime)));
        agent.speed = 4 + 2*agent.remainingDistance;
    }
}
