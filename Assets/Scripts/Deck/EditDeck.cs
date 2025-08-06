using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCards: MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject background;
    public GameObject deckMenu;
    List<string> cardsSelling = new List<string>();
    List<string> activeSelling = new List<string>();
    public Deck deck;
    private void Awake()
    {
        GenerateCards();
        foreach (Transform item in deckMenu.transform)
        {
            activeSelling.Add(item.name);
        }
    }
    void Interact(Transform player_)
    {
        player_.GetComponent<PlayerMovement>().actions.Disable();
        background.SetActive(true);
        deckMenu.SetActive(true);
        if(name=="BuyCards")GenerateCards(1);
    }
    public bool CheckCardLimit(string cardName)
    {
        int count = 0;
        foreach(string i in deck.cardList)
        {
            if (i == cardName) count++;
        }
        foreach (string i in deck.tempCardList)
        {
            if (i == cardName) count++;
        }
        foreach (string i in activeSelling)
        {
            if (i == cardName) return false;
        }
        foreach (string i in cardsSelling)
        {
            if (i == cardName) count++;
        }
        return (count < 5);

    }
    public void GenerateCards()
    {
        List<int> attemptedCards=new List<int>();
        int i;
        foreach(Transform child in deckMenu.transform)
        {
            ManagePurchace purchace = child.GetComponent<ManagePurchace>();
            if (cardsSelling.Count < 8)
            {
                i = Random.Range(2, deck.allCards.Count - attemptedCards.Count);
                foreach (int j in attemptedCards)
                {
                    if (j! > i) i++;
                }
                if (CheckCardLimit(deck.allCards[i])==true)
                {
                    cardsSelling.Add(deck.allCards[i]);
                }
                else attemptedCards.Add(i);
            }
            if (child.gameObject.activeInHierarchy)
            {
                try
                {

                    if (purchace.card == "null")
                    {
                        if (cardsSelling.Count == 0) 
                        {
                            purchace.card = "Key";
                            purchace.price = 5;
                        }
                        else
                        {
                            purchace.card = cardsSelling[child.GetSiblingIndex()];
                            activeSelling[child.GetSiblingIndex() - 2] = cardsSelling[child.GetSiblingIndex()];
                            purchace.setPrice(child.GetComponent<ManagePurchace>().card);
                            cardsSelling.RemoveAt(0);
                        }
                    }

                }
                catch{}
            }
        }
    }
    public void GenerateCards(int k)
    {
        List<int> attemptedCards = new List<int>();
        int i;
        foreach (Transform child in deckMenu.transform)
        {
            ManagePurchace purchace = child.GetComponent<ManagePurchace>();
            if (cardsSelling.Count < 1)
            {
                i = Random.Range(2, deck.allCards.Count - attemptedCards.Count);
                foreach (int j in attemptedCards)
                {
                    if (j! > i) i++;
                }
                if (CheckCardLimit(deck.allCards[i]))
                {
                    cardsSelling.Add(deck.allCards[i]);
                }
                else attemptedCards.Add(i);
            }
            try
            {
                if (purchace.card == "null")
                {
                    purchace.card = cardsSelling[0];
                    purchace.setPrice(child.GetComponent<ManagePurchace>().card);
                    cardsSelling.RemoveAt(0);
                }
            }
            catch{}
        }
    }
    public string thisCard(int i)
    {
        GenerateCards();
        string selectedCard = cardsSelling[i];
        cardsSelling.RemoveAt(i);
        return selectedCard;
    }
    public void AddToDeck(string cardName)
    {
        deck.cardList.Add(cardName);
    }
}
