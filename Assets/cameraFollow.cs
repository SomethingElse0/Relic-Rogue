using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    NavMeshAgent agent;
    Vector3 destination;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.speed = 5;
    }
    // Update is called once per frame
    void Update()
    {
        destination= new Vector3(player.position.x, player.position.y, transform.position.z);
        if ((destination - transform.position).magnitude <2000&& (destination - transform.position).magnitude >1)
        {
            agent.SetDestination(destination);
            agent.speed = 4 + 2 * agent.remainingDistance;
        }
        else agent.speed = player.GetComponent<PlayerMovement>().playerData.playerSpeed;
    }
}
