using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HPTracker : MonoBehaviour
{
    // Start is called before the first frame update
    Enemy1 enemy;
    public Transform newLocation;
    void Awake()
    {
        try {
            if (enemy.transform.name != newLocation.name) 
            { 
                enemy = transform.parent.parent.GetComponent<Enemy1>(); 
            }
        }
        catch { enemy = transform.parent.parent.GetComponent<Enemy1>(); }
        enemy.tracker = transform.GetComponent<HPTracker>();
        transform.parent.SetParent(newLocation);
        enemy.hp = enemy.maxHP;
        transform.rotation = newLocation.rotation;
    }

    // Update is called once per framee
    void Update()
    {
        
        transform.localScale = new Vector3(enemy.hp / enemy.maxHP, transform.localScale.y, transform.localScale.z);
        if (enemy.gameObject.activeInHierarchy) transform.parent.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y+0.5f, enemy.transform.position.z-0.5f);
        else transform.parent.SetParent(enemy.transform);
    }
}
