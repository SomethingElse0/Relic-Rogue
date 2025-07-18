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
    List<Vector3> patrolPoints = new List<Vector3>();
    float newPatrolPointTime;
    bool playerClose;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolPoints.Add(transform.position);
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
            Ray newRay = new Ray(transform.position,Vector3.Normalize(player.transform.position-transform.position));
            if (Physics.Raycast(newRay, 5)) 
            {
                lineOfSight = false;
                agent.speed = 1f;
                if (playerClose) player.GetComponent<PlayerMovement>().closeEnemies--;
                playerClose = false;
            }
            else 
            { 
                lineOfSight = true; 
                agent.speed = 7;
                if (!playerClose) player.GetComponent<PlayerMovement>().closeEnemies++;
                playerClose = true;
            }
            if (Vector3.Magnitude(player.transform.position - transform.position) < 10 && lineOfSight != false)
            {
                agent.SetDestination(player.transform.position);
                if (!playerClose)player.GetComponent<PlayerMovement>().closeEnemies++;
                playerClose = true;
                if (agent.remainingDistance < 10 && agent.velocity.magnitude == 0)
                {
                    agent.updateRotation = false;
                    transform.LookAt(player.transform);
                    agent.updateRotation = true;
                    if ((player.transform.position - transform.position).normalized == transform.forward && Time.fixedTime > attackCooldown + attackTime) Attack();
                }
            }
            else if (agent.remainingDistance < 1)
            {
                if (playerClose) player.GetComponent<PlayerMovement>().closeEnemies--;
                playerClose = false;
                int patrolPointNo = Random.Range(0, 10);
                try
                {
                    agent.SetDestination(patrolPoints[patrolPointNo]);
                }//a nice way of saying that the enemy will sometimes pick a random destination
                catch
                {
                    Vector3 newDestination = new Vector3(Random.Range(-7, 7), Random.Range(-7, 7), 0) + transform.position;
                    agent.SetDestination(newDestination);
                    if ((patrolPoints[patrolPoints.Count - 1] - newDestination).magnitude > 2)
                    {
                        patrolPoints.Add(transform.position);
                        int i = Random.Range(1, patrolPoints.Count);
                        if (patrolPoints.Count > 8) patrolPoints.RemoveAt(i);
                    }
                }
            }
            
        }
        else
        {
            if (playerClose) player.GetComponent<PlayerMovement>().closeEnemies--;
            playerClose = false;
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
        if ((patrolPoints[patrolPoints.Count - 1] - transform.position).magnitude > 2)
        {
            patrolPoints.Add(transform.position);
            int i = Random.Range(1, patrolPoints.Count);
            if (patrolPoints.Count > 8) patrolPoints.RemoveAt(i);
        }
    }
    void OnHit(float damage)
    {
        hp -= damage;
    }
}
