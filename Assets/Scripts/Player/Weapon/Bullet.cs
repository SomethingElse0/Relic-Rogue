using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    int bounces=100;
    public Deck deck;
    WeaponData data;
    Rigidbody rb;
    float damagePrivate;
    public float damage;
    Transform parent;
    float maxSpeed = 12;
    List<RaycastHit> hitList = new List<RaycastHit>();
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(transform.parent.name=="Weapon")data = transform.parent.GetComponent<WeaponSystem>().GunData;
        if (transform.parent.CompareTag(tag)) parent = transform.parent;
    }

    // Update is called once per frame
    public void Bounces(int i, int speed, float newDamage)
    {
        Vector3 directionFacing = -transform.GetChild(0).position+ transform.position;
        damagePrivate = newDamage;
        rb.AddForce(directionFacing.normalized * speed);
        damage = damagePrivate;
    }
    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            collision.gameObject.SendMessage("OnHit", damage + deck.damageModifier, SendMessageOptions.DontRequireReceiver);
        }
        catch { }
        if(deck!=null)if (data.bulletType == "Explosive") Explode();
        bounces--;
        damagePrivate--;
        if (bounces < 1) Destroy(gameObject,0.1f);
        if (damagePrivate == 0) Destroy(gameObject, 0.1f);
        damage = damagePrivate;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (collision.hasComponent(reflector)){damagePrivate+=2; rb.velocity= \reflector.direction*rb.velocity.magnitude;}
    }
    void Explode()
    {
        Ray newRay = new Ray(transform.position, new Vector3(0, 0, 0));
        hitList.AddRange(Physics.SphereCastAll(newRay,5));
        foreach (RaycastHit hit in hitList)
        { 
            hit.transform.SendMessage("OnHit", damage * (1 + (5 / hit.distance)), SendMessageOptions.DontRequireReceiver);
            print("BOOM");
        }
        Destroy(gameObject);
    }
}
