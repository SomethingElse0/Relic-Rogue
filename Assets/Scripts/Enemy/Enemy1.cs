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
    float hp=40;
    int maxHP = 40;
    bool hasAggro;
    bool lineOfSight=false;
    Vector3 startPosition;
    Light _light;
    
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        bullet = transform.GetChild(0).gameObject;
        bullet.SetActive(false);
        bulletHolder = transform.parent.GetChild(0).gameObject;
        _light = transform.GetChild(1).GetComponent<Light>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //basically a line of sight thing
        if (hp > 0)
        {
            agent.SetDestination(player.transform.position);
            if (agent.remainingDistance < (player.transform.position - transform.position).magnitude + 3 && (agent.destination - player.transform.position).magnitude < 0.1f)
            {
                agent.speed = 7f;
                lineOfSight = true;
            }
            else
            {
                agent.speed = 1;
                lineOfSight = false;
            }
            if (agent.remainingDistance < 10 && agent.velocity.magnitude == 0)
            {
                agent.updateRotation = false;
                transform.LookAt(player.transform);
                agent.updateRotation = true;
                if ((player.transform.position - transform.position).normalized == transform.forward && Time.fixedTime > attackCooldown + attackTime) Attack();
            }
            
        }
        else
        {
            agent.speed = 0;
            agent.Warp(startPosition);
            Component pause =gameObject.AddComponent<Paused>();
            Destroy(pause, 5);
            hp = maxHP;
            agent.speed = 5f;

        }
        _light.intensity = 1+(2 * Mathf.Cos(Time.fixedTime));
    }
    void Attack()
    {
        GameObject newBullet = Instantiate(bullet, transform.position+(player.transform.position-transform.position).normalized, transform.rotation, transform.parent.GetChild(0));
        attackTime = Time.fixedTime;
        newBullet.SetActive(true);
        newBullet.GetComponent<Bullet>().Bounces(3, transform.position, 5);
        
    }
}
