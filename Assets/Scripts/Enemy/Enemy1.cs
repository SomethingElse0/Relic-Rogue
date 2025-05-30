using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    GameObject bullet;
    float attackCooldown=3;
    float attackTime=-100;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bullet = transform.GetChild(0).gameObject;
        bullet.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //basically a line of sight thing
        agent.SetDestination(player.transform.position);
        if (agent.remainingDistance < (player.transform.position - transform.position).magnitude + 3 && (agent.destination-player.transform.position).magnitude<1) agent.speed = 12f;
        else agent.speed = 1;
        print(agent.velocity.magnitude);
        if (agent.remainingDistance < 10&&agent.velocity.magnitude==0 && Time.fixedTime > attackCooldown + attackTime) Attack();
    }
    void Attack()
    {
        GameObject newBullet = Instantiate<GameObject>(bullet, transform.position+(player.transform.position-transform.position).normalized, transform.rotation);
        attackTime = Time.fixedTime;
        newBullet.SetActive(true);
        newBullet.GetComponent<Bullet>().Bounces(3, player.transform, 5);
        
    }
}
