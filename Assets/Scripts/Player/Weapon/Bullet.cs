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
    List<RaycastHit> hitList = new List<RaycastHit>();
    int baseSpeedMult = 3;
    Vector3 directionFacing;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(transform.parent.name=="Weapon")data = transform.parent.GetComponent<WeaponSystem>().GunData;
        if (transform.parent.CompareTag(tag)) parent = transform.parent;
        
    }
    // Update is called once per frame
    private void Update()
    {
        if (rb.linearVelocity.normalized != directionFacing) rb.linearVelocity = rb.linearVelocity.normalized * baseSpeedMult;
        if (directionFacing.magnitude == 1) directionFacing = rb.linearVelocity.normalized;
    }
    
    public void Bounces(Vector3Int newInput)//setting the speed etc. I used this vector3 input so I could send 3 integer inputs through send message
    {
        int i = Mathf.Abs(newInput.x);
        int speed = newInput.y;
        int newDamage = newInput.z;
        directionFacing = (transform.GetChild(0).position - transform.position).normalized;
        damagePrivate = newDamage;
        baseSpeedMult += speed;
        rb.linearVelocity = directionFacing.normalized * baseSpeedMult;
        damage = damagePrivate;
    }
    public void Bounces(int i, int speed, float newDamage)
    {
        directionFacing = (transform.GetChild(0).position- transform.position).normalized;
        damagePrivate = newDamage;
        baseSpeedMult += speed;
        rb.linearVelocity=(directionFacing.normalized * baseSpeedMult);
        damage = damagePrivate;
    }

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            collision.gameObject.SendMessage("OnHit", damage + deck.damageModifier, SendMessageOptions.DontRequireReceiver);//attempts to deal damage
            if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy"&&bounces>0) bounces--;
            else Destroy(gameObject);
            if (bounces > 0 && damagePrivate > 0)
            {
                bounces--;
                damagePrivate--;
            }
            else Destroy(gameObject);//cannot bounce indefinitely
        }
        catch 
        {
            if (bounces > 0 && damagePrivate > 0)
            {
                bounces--;
                damagePrivate--;
            }
            else Destroy(gameObject);

        }
        try
        {
            if (transform.parent.name.ToLower() != "weapon")
            {
                if (data.bulletType == "Explosive") Explode();
            }
        }
        catch { }
        rb.linearVelocity += Random.Range(-0.1f, 0.1f) * collision.impulse.normalized;
        
        damagePrivate--;
        damage = damagePrivate;
    }
    /*private void OnTriggerEnter(Collider col)
    {
        try{rb.velocity= col.GetComponent<Reflect>().direction*rb.velocity.magnitude;}
        catch{}
    }//THIS IS NOT YET IMPLEMENTED
    */
    void Explode()//this is  an AOE damage effect
    {
        baseSpeedMult = 0;
        rb.linearVelocity = Vector3.zero;
        transform.GetChild(1).gameObject.SetActive(true);
        Ray newRay = new Ray(transform.position, new Vector3(0, 0, 0));
        hitList.AddRange(Physics.SphereCastAll(newRay,5));
        foreach (RaycastHit hit in hitList)
        { 
            hit.transform.SendMessage("OnHit", damage * (1 + (5 / hit.distance)), SendMessageOptions.DontRequireReceiver);
            print("BOOM");
        }
        Destroy(gameObject, 0.5f);
    }
}
