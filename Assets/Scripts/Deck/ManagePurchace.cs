using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePurchace : MonoBehaviour
{
    public int price;
    public string card;
    public PlayerData data;
    Deck deck;
    void Awake()
    {
        price = setPrice(card);
    }
    public int setPrice(string cardName)
    {
        int count = 1;
        foreach (string i in deck.cardList)
        {
            if (i == cardName) count++;
        }
        int cardNumber = deck.allCards.LastIndexOf(cardName);
        int price = 5 + 2 * (cardNumber + 3 + (count * 5)) * (3 * cardNumber + 1) - 2 * count;
        return price;
    }
    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Button>().interactable = data.coins > price;
    }
    public void OnPurchace()
    {
        data.coins -= price;
        deck.allCards.Add(card);
    }
}
