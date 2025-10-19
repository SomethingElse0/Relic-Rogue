using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckRandomiser : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck originalDeck;
    public Deck deck;
    List<string> list = new List<string>();
    public string lastCard;
    public int numberOfCards = 0;
    public int cardTolerance=0;//basically, the number of cards allowed to build up
    float cooldown=10;
    private void Awake()
    {
        deck.player = originalDeck.player;//setting variables
        deck.weapon = originalDeck.weapon;
        deck.generator = originalDeck.generator;
        deck.deck = originalDeck.deck;
        deck.cardList = originalDeck.cardList;
        ScrambleDeck();//scrambling the deck
        
    }
    public void ScrambleDeck()//shuffles the deck
    {
        list.Clear();
        list.AddRange(originalDeck.tempCardList);
        List<string> ScrambledList = new List<string>();
        foreach (string str in list) AddToDeck(ScrambledList, str); 
        deck.tempCardList=ScrambledList;
        Invoke("CardSelect", 10);
    }

    // Update is called once per frame
    public void AddToDeck(List<string> list, string item)//adds to a random position in the deck
    {
        int itemPosition = Random.Range(0, list.Count);
        list.Insert(itemPosition, item);
    }
    public void CardSelect()//picking and activating a random card from the deck
    {
        cooldown = Time.fixedTime + 30;
        lastCard = deck.tempCardList[0].ToString();
        
        if (numberOfCards > cardTolerance)
        {
            
            gameObject.BroadcastMessage(deck.tempCardList[0], SendMessageOptions.DontRequireReceiver);//this is so that I don't have to use a bunch of if statements or scripts for cards, I can just add the function, and the nme to the card list, and it will work - easily expandable
            deck.tempCardList.RemoveAt(0);
            numberOfCards--;
            Invoke("CardSelect", 5);
        }
        if (deck.tempCardList.Count < 5) AddTrapCards(2);
        
    }
    public void CardSelect(string origin)// different version of the above
    {
        cooldown = Time.fixedTime + 20;
        lastCard = deck.tempCardList[0].ToString();
        if (numberOfCards > cardTolerance)
        {
            float waitTime = Time.fixedTime + 2;

            gameObject.BroadcastMessage(deck.tempCardList[0], SendMessageOptions.DontRequireReceiver);
            deck.tempCardList.RemoveAt(0);
            numberOfCards--;
            Invoke("CardSelect", waitTime);
        }
        else transform.SendMessage("Part2", SendMessageOptions.DontRequireReceiver);
        if (deck.tempCardList.Count < 5) AddTrapCards(3);
        
    }
    public void AddTrapCards(int i)//this is so that the deck is never empty
    {
        while (i > 0)
        {
            AddToDeck(deck.tempCardList, deck.trapCards[Random.Range(0, deck.trapCards.Count)]);
            i--;
        }
    }
}
