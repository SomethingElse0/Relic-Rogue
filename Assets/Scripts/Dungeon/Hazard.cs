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
            timer += 40;
            OnHazard();
        }
    }
    void OnHazard()
    {
        int i = Random.Range(25, 50);
        int j = Random.Range(0, transform.childCount-hazardsTriggered);
        if (i < deck.hazardProb)
        {
            try
            {
                transform.GetChild(j).gameObject.GetComponent<NavMeshObstacle>().enabled = false;
            }
            catch
            {
                transform.GetChild(j).gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            transform.GetChild(j).SetAsLastSibling();
            hazardsTriggered++;
        }
        else deck.hazardProb++;
    }
}
