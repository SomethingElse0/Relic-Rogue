using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePurchace : MonoBehaviour
{
    public int price;
    public string card;
    public PlayerData data;
    public Deck deck;
    public void setPrice(string cardName)
    {
        int count = 1;
        foreach (string i in deck.cardList)
        {
            if (i == cardName) count+=count;
        }
        foreach (string i in deck.tempCardList)
        {
            if (i == cardName) count += count;
        }
        foreach (Transform child in transform.parent)
        {
            try
            {
                if (child.GetComponent<ManagePurchace>().card == card && child.GetSiblingIndex() < transform.GetSiblingIndex()) count += 3+count;
            }
            catch { }
        }
        int cardNumber = deck.allCards.LastIndexOf(cardName);
        int newPrice = (cardNumber*count)*3 + (10 * transform.GetSiblingIndex() - 1) - 2 * count;
        price = newPrice;
    }
    public void setPrice()
    {
        int count = 1;
        foreach (string i in deck.cardList)
        {
            if (i == card) count += count;
        }
        foreach (string i in deck.tempCardList)
        {
            if (i == card) count += count;
        }
        foreach (Transform child in transform.parent)
        {
            try
            {
                if (child.GetComponent<ManagePurchace>().card == card && child.GetSiblingIndex() < transform.GetSiblingIndex()) count += 3 + count;
            }
            catch { }
        }
        int cardNumber = deck.allCards.LastIndexOf(card);
        int newPrice = (cardNumber * count) * 3 + (10 * transform.GetSiblingIndex() - 1) - 2 * count;
        price = newPrice;
    }
    // Update is called once per frame
    void Update()
    {
        if (card == "null") 
        {
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Temporarily out of stock";
            price = 5;
            transform.GetComponent<Button>().interactable = false;
        }        
        else if (card == "Key")transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = card + " : 5";
        else
        {
            setPrice();
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = card + " : " + price;
            transform.GetComponent<Button>().interactable = data.coins > price;
        }
        
    }
    public void OnPurchace()
    {
        data.coins -= price;
        if (card == "Key") data.keys++;
        else deck.cardList.Add(card);
        card = "null";
    }
}
