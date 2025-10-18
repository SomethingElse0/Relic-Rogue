using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    public GameObject player;
    public HPTracker tracker;
    float attackCooldown=5;
    float attackTime=10;
    float attackEndTime;
    public float hp=20;
    public int maxHP = 20;
    bool hasAggro;
    bool lineOfSight=false;
    Vector3 startPosition;
    Quaternion startRotation;
    Light _light;
    List<Vector3> patrolPoints = new List<Vector3>();
    float newPatrolPointTime;
    bool playerClose;
    float respawnTime = -1;
    bool attacking=false;

    // Start is called before the first frame update
    private void Awake()
    {
        startRotation = transform.rotation;
        agent = transform.parent.GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        patrolPoints.Add(transform.position);
        _light = transform.GetChild(1).GetComponent<Light>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //basically a line of sight thing
        if (Time.fixedTime < attackEndTime) rb.velocity = rb.velocity.normalized * 8;
        else
        {
            if (attacking==true)
            {
                transform.rotation = startRotation;
                rb.velocity = Vector3.zero;
                agent.enabled = true;
                agent.Warp(new Vector3(transform.position.x, transform.position.y, transform.parent.position.z));
                transform.position = Vector3.zero;
                attacking = false;
            }
            if (hp > 0 && Time.deltaTime > respawnTime)
            {
                agent.speed = 3;
                Ray newRay = new Ray(transform.position, player.transform.position - transform.position);
                Physics.Raycast(newRay, out RaycastHit hit);
                if (hit.transform != player.transform)
                {
                    lineOfSight = false;
                    if (playerClose) player.GetComponent<PlayerMovement>().closeEnemies--;
                    playerClose = false;
                }
                else
                {
                    lineOfSight = true;

                    if (!playerClose) player.GetComponent<PlayerMovement>().closeEnemies++;
                    patrolPoints.Add(transform.position);
                    playerClose = true;
                }
                if (!(Vector3.Magnitude(player.transform.position - agent.destination) > 1 && Vector3.Magnitude(player.transform.position - transform.position) > 10) && lineOfSight)
                {
                    agent.SetDestination(player.transform.position);
                    if (!playerClose) player.GetComponent<PlayerMovement>().closeEnemies++;
                    playerClose = true;
                    if (agent.remainingDistance < 10 && agent.velocity.magnitude == 0)
                    {
                        agent.updateRotation = false;
                        transform.LookAt(player.transform);
                        agent.updateRotation = true;
                        if (Time.fixedTime > attackCooldown + attackEndTime) Attack();
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
                else agent.SetDestination(agent.destination);
            }

            else
            {
                if (playerClose) player.GetComponent<PlayerMovement>().closeEnemies--;
                if (hp! > 0) respawnTime = Time.fixedTime + 8;
                playerClose = false;
                agent.speed = 0;
                agent.Warp(new Vector3(startPosition.x, startPosition.y, transform.position.z));
                hp = maxHP;
                agent.speed = 3f;
            }
        }
        _light.intensity = 1+(2 * Mathf.Cos(Time.fixedTime));
    }
    void Attack()
    {
        agent.ResetPath();
        attackEndTime = attackTime+Time.fixedTime;
        transform.LookAt(player.transform);
        
        rb.velocity = (player.transform.position-transform.position).normalized * 8;
        
        if ((patrolPoints[patrolPoints.Count - 1] - transform.position).magnitude > 2)
        {
            patrolPoints.Add(transform.position);
            int i = Random.Range(1, patrolPoints.Count);
            if (patrolPoints.Count > 8) patrolPoints.RemoveAt(i);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player) player.GetComponent<PlayerMovement>().OnHit(3);
        rb.velocity += (player.transform.position - transform.position).normalized*0.5f;
    }
    public void OnHit(float damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            patrolPoints.Add(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            transform.position = startPosition;
            hp = maxHP;
        }
    }
}
