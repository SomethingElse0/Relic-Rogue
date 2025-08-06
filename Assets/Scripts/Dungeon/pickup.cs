using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    // Start is called before the first frame update
    float startTime;
    private void Awake()
    {
        Destroy(gameObject, 300);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            if (transform.name == "Scrap") col.transform.GetComponent<PlayerMovement>().scrap+=Random.Range(2,7);
            else if (transform.name == "Coin") col.transform.GetComponent<PlayerMovement>().deck.coin+=Random.Range(3,9);
            else if (transform.name == "Fuel") col.transform.GetComponent<PlayerMovement>().deck.fuel+=Random.Range(6,17);
            else if (transform.name == "Ration") col.transform.GetComponent<PlayerMovement>().rations+=Random.Range(1,4);
            else if (transform.name == "LevelKey") col.transform.GetComponent<PlayerMovement>().levelKey =true;
            Destroy(gameObject);
        }
    
        
    }

    // Update is called once per frame
}
