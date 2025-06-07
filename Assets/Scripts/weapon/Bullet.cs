using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    int bounces=100;
    Rigidbody rb;
    float damagePrivate;
    public float damage;
    Transform parent;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (transform.parent.CompareTag(tag)) parent = transform.parent;
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    public void Bounces(int i, Transform target, float newDamage)
    {
        damagePrivate = newDamage;
        bounces = i;
        if (target.position==parent.position) rb.velocity = (10 * (transform.position - target.position).normalized);
        else rb.velocity=(10*(target.position-transform.position).normalized);
        damage = damagePrivate;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if 
        bounces--;
        damagePrivate--;
        if (bounces < 0) Destroy(gameObject,0.1f);
        if (damagePrivate == 0) Destroy(gameObject, 0.1f);
        damage = damagePrivate;
    }
}
