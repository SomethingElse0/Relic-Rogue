using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hazard : MonoBehaviour
{
    // Start is called before the first frame update

    public Deck deck;
    float timer=60;
    int hazardsTriggered=0;
    // Update is called once per frame
    void Update()
    {
        if (timer < Time.fixedTime)
        {
            timer = Time.fixedTime +20;
            OnHazard();
        }
    }
    void OnHazard()//closes off paths for the player, and opens paths for enemies over time
    {
        int i = Random.Range(25, 50);
        int j = Random.Range(0, transform.childCount-hazardsTriggered);
        if (i < deck.hazardProb)
        {
            
            try
            {
                transform.GetChild(j).gameObject.GetComponent<NavMeshObstacle>().enabled = false;//allows the enemies another path, andd into more areas
            }
            catch
            {
                transform.GetChild(j).gameObject.GetComponent<BoxCollider>().enabled = true;// restricts where the player can go
            }
            transform.GetChild(j).SetAsLastSibling();
            hazardsTriggered++;//to ensure it is not triggered twice
        }
        else deck.hazardProb++;
    }
}
