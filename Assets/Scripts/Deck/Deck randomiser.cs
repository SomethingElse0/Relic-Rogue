using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckRandomiser : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck originalDeck;
    public Deck deck;
    Cards cards;
    List<string> list = new List<string>();
    public int numberOfCards = 0;
    public int cardTolerance=0;
    private void Awake()
    {
        deck.player = originalDeck.player;  
        deck.weapon = originalDeck.weapon;
        deck.generator = originalDeck.generator;
        deck.deck = originalDeck.deck;
        deck.cardList = originalDeck.cardList;
        ScrambleDeck();
    }
    public void ScrambleDeck()
    {
        list.Clear();
        list.AddRange(originalDeck.tempCardList);
        List<string> ScrambledList = new List<string>();
        
        int i = 0;
        while (i < list.Count)
        {
            int SelectedItemNo = Random.Range(0, list.Count-1);
            AddToDeck(ScrambledList, list[SelectedItemNo]);
            list.RemoveAt(SelectedItemNo);
            i++;
        }
        deck.tempCardList=ScrambledList;
    }

    // Update is called once per frame
    public void AddToDeck(List<string> list, string item)
    {
        int itemPosition = Random.Range(0, list.Count);
        list.Insert(itemPosition, item);
    }
    public void CardSelect()
    {
        
        //deck.tempCardList[0];
        
        if (numberOfCards > cardTolerance)
        {
            float waitTime = Time.fixedTime + 2;
            
            gameObject.BroadcastMessage(deck.tempCardList[0], SendMessageOptions.DontRequireReceiver);
            deck.tempCardList.RemoveAt(0);
            numberOfCards--;
            Invoke("CardSelect", waitTime);
        }
        if (deck.tempCardList.Count < 5) AddTrapCards(2);
    }
    public void CardSelect(string origin)
    {

        //deck.tempCardList[0];

        if (numberOfCards > cardTolerance)
        {
            float waitTime = Time.fixedTime + 2;

            transform.SendMessage("Awake", SendMessageOptions.DontRequireReceiver);
            deck.tempCardList.RemoveAt(0);
            numberOfCards--;
            Invoke("CardSelect", waitTime);
        }
        else transform.SendMessage("Part2", SendMessageOptions.DontRequireReceiver);
    }
    public void AddTrapCards(int i)
    {
        while (i > 0)
        {
            AddToDeck(deck.tempCardList, deck.trapCards[Random.Range(0, deck.trapCards.Count - 1)]);
        }
    }
}
