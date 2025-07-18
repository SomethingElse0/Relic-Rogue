using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoTheDepths : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck deck;
    void Awake()
    {
        deck.generator.GetComponent<GenerateScrap>().GenerateSpecific("key");
        deck.tempCardList.RemoveAt(1);
    }

    // Update is called once per frame
}
