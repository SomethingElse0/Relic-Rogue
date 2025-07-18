using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseCollapse : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    

    private void Awake()
    {
        target.hazardProb = Mathf.Abs(target.hazardProb);
        target.hazardProb += 3;
        target.deck.GetComponent<DeckRandomiser>().numberOfCards += 2;
        target.deck.GetComponent<DeckRandomiser>().CardSelect();
    }
    void Part2()
    {
        target.hazardProb = -Mathf.Abs(target.hazardProb);
    }

}
