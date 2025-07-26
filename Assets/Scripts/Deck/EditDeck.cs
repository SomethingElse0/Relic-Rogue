using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCards: MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject background;
    public GameObject deckMenu;
    public GameObject player;
    List<string> cardsSelling = new List<string>();
    Deck deck;
    int count;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.SendMessage("OnDisable", SendMessageOptions.DontRequireReceiver);
            background.SetActive(true);
            deckMenu.SetActive(true);
        }

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
    public void GenerateCards()
    {
        List<int> attemptedCards=new List<int>();
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
    public string thisCard(int i)
    {
        GenerateCards();
        string selectedCard = cardsSelling[i];
        cardsSelling.RemoveAt(i);
        return selectedCard;
    }
    public int setPrice(string cardName)
    {
        int count=1;
        foreach (string i in deck.cardList)
        {
            if (i == cardName) count++;
        }
        int cardNumber = deck.allCards.LastIndexOf(cardName);
        int price = 5 + 2 * (cardNumber + 3 + (count*5)) * (3*cardNumber + 1) - 2*count;
        return price;
    }
    public void AddToDeck(string cardName)
    {
        deck.cardList.Add(cardName);
    }
}
