using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (enemy.gameObject.activeInHierarchy) transform.parent.position = enemy.transform.position + new Vector3(0,0.7f,-2);
        else transform.parent.SetParent(enemy.transform);
    }
}
