using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HPTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy1 enemy1;
    Enemy2 enemy2;
    int enemyType;
    public Transform newLocation;
    void Awake()
    {
        try
        {
                
            enemy1 = transform.parent.parent.GetComponent<Enemy1>();
            enemy1.hp = enemy1.maxHP;
            enemyType = 1;
        }
        catch 
        { 
            enemy2 = transform.parent.parent.GetChild(0).GetComponent<Enemy2>();
            enemyType = 2;
            enemy2.hp = enemy2.maxHP;
        }
        transform.parent.SetParent(newLocation);        
        transform.rotation = newLocation.rotation;
    }

    // Update is called once per framee
    void Update()
    {
        if (enemyType == 1)
        {
            Enemy1 enemy = enemy1;
            transform.localScale = new Vector3(enemy.hp / enemy.maxHP, transform.localScale.y, transform.localScale.z);
            if (enemy.gameObject.activeInHierarchy) transform.parent.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 0.5f, enemy.transform.position.z - 0.5f);
            else transform.parent.SetParent(enemy.transform);
        }
        else if (enemyType == 2)
        {
            Enemy2 enemy = enemy2;
            transform.localScale = new Vector3(enemy.hp / enemy.maxHP, transform.localScale.y, transform.localScale.z);
            if (enemy.gameObject.activeInHierarchy) transform.parent.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 0.5f, enemy.transform.position.z - 0.5f);
            else transform.parent.SetParent(enemy.transform);
        }
    }
}
