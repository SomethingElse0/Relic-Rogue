using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    int bounces=100;
    Rigidbody rb;
    float damagePrivate;
    public float damage;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void Update()
    {
        damage = damagePrivate;
    }
    // Update is called once per frame
    public void Bounces(int i, Transform target, float newDamage)
    {
        damagePrivate = newDamage;
        bounces = i;
        rb.velocity=(10*(target.position-transform.position).normalized);
    }
    private void OnCollisionEnter(Collision collision)
    {
        bounces--;
        damage--;
        if (bounces < 0) Destroy(gameObject);
    }
}
