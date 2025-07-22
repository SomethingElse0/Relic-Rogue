using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    int bounces=100;
    public Deck deck;
    Rigidbody rb;
    float damagePrivate;
    public float damage;
    Transform parent;
    float maxSpeed = 12;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (transform.parent.CompareTag(tag)) parent = transform.parent;
    }

    // Update is called once per frame
    public void Bounces(int i, Vector3 direction, float newDamage)
    {
        damagePrivate = newDamage;
        bounces = i;
        rb.AddForce(direction*5);
        damage = damagePrivate;
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("OnHit", damage + deck.damageModifier, SendMessageOptions.DontRequireReceiver);
        bounces--;
        damagePrivate--;
        if (bounces < 0) Destroy(gameObject,0.1f);
        if (damagePrivate == 0) Destroy(gameObject, 0.1f);
        damage = damagePrivate;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (collision.hasComponent(reflector)){damagePrivate+=2; rb.velocity= \reflector.direction*rb.velocity.magnitude;}
    }
}
