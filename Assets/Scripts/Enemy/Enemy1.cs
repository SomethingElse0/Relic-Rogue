using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject player;
    GameObject bullet;
    GameObject bulletHolder;
    float attackCooldown=5;
    float attackTime=10;
    bool hasAggro;
    bool lineOfSight=false;
    Light light;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bullet = transform.GetChild(0).gameObject;
        bullet.SetActive(false);
        bulletHolder = transform.parent.GetChild(0).gameObject;
        light = transform.GetChild(1).GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        //basically a line of sight thing
        agent.SetDestination(player.transform.position);
        if (agent.remainingDistance < (player.transform.position - transform.position).magnitude + 3 && (agent.destination - player.transform.position).magnitude<0.1f)
        {
            agent.speed = 7f;
            lineOfSight = true;
        }
        else 
        {
            agent.speed = 1; 
            lineOfSight = false;
        }
        print(agent.velocity.magnitude);
        if (agent.remainingDistance < 10 && agent.velocity.magnitude == 0 && Time.fixedTime > attackCooldown + attackTime && lineOfSight == true)
        {
            if((player.transform.position-transform.position).normalized==transform.forward)Attack();
        }
        light.intensity = 1+(2 * Mathf.Cos(Time.fixedTime));
    }
    void Attack()
    {
        GameObject newBullet = Instantiate(bullet, transform.position+(player.transform.position-transform.position).normalized, transform.rotation);
        attackTime = Time.fixedTime;
        newBullet.SetActive(true);
        newBullet.GetComponent<Bullet>().Bounces(3, player.transform, 5);
        
    }
}
