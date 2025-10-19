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
    public void setPrice(string cardName)//setting the price of each card
    {
        int count = 1;
        foreach (string i in deck.cardList)//taking into account how many coppies of the card the player already has
        {
            if (i == cardName) count+=count;
        }
        foreach (string i in deck.tempCardList)//taking into account how many coppies of the card the player already has in their deck
        {
            if (i == cardName) count += count;
        }
        if(count>3)
        foreach (Transform child in transform.parent)
        {
            try
            {
                if (child.GetComponent<ManagePurchace>().card == card && child.GetSiblingIndex() < transform.GetSiblingIndex()) count += 3+count;
            }
            catch { }
        }
        if (count > 3) card = "Key";
        int cardNumber = deck.allCards.LastIndexOf(cardName);
        int newPrice;
        if (card == "Key") newPrice = 10;
        else newPrice = (cardNumber*count)*3 + (10 * transform.GetSiblingIndex() - 1) - 2 * count;
        price = newPrice;
    }
    // Update is called once per frame
    void Update()
    {
        if (card == "null") //when the player 
        {
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Card Successfully Purchased!";
            transform.GetComponent<Button>().interactable = false;
        }        
        else
        {
            setPrice(card);
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = card + " : " + price;
            transform.GetComponent<Button>().interactable = data.coins > price;//if the player can afford the item, they can buy it
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
