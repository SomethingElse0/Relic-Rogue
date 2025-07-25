using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditDeck : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform background;
    public Transform deckEditMenu;
    public GameObject player;
    Deck deck;
    int count;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player) { player.SendMessage("OnDisable", SendMessageOptions.DontRequireReceiver); }

    }

    // Update is called once per frame
    public bool CheckCardLimit(string cardName)
    {
        deck = player.GetComponent<PlayerMovement>().deck;
        count = 0;
        foreach(string i in deck.cardList)
        {
            if (i == cardName) count++;
        }
        if (deck.cardList.Capacity > 24) count += 10;
        if (count > 5) return false;
        else return true;

    }
    public void PurchaceCards()
    {
        List<int> attemptedCards=new List<int>();
        List<string> cardsSelling=new List<string>();
        int i;
        while (cardsSelling.Count < 3&&cardsSelling.Count>attemptedCards.Count) 
        {
            i = Random.Range(0, deck.allCards.Capacity - attemptedCards.Count);
            foreach(int j in attemptedCards)
            {
                if (j! > i) i++;
            }
            if (CheckCardLimit(deck.allCards[i]))
            {
                cardsSelling.Add(deck.allCards[i]);
            }
            attemptedCards.Add(i);
        }
    }

}
