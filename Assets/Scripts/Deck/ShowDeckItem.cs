using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeckItem : MonoBehaviour
{
    public Deck deck;
    string cardName;
    public bool inDeck;
    public GameObject Otherlist;
    int tempCardListLength;
    int spareCardListLength;
    Transform card;
    int intIfInDeck(bool b)// I'm using this to simplify the logic, so that I don't have to write the code twice.
    {
        if (inDeck==b) return tempCardListLength;
        else return spareCardListLength;
    }
    GameObject sibling(int i)
    {
        return transform.parent.parent.GetChild(i).gameObject;
    }
    private void Awake()
    {
        card = transform.parent;//getting variables 
        tempCardListLength = deck.tempCardList.Count;//getting variables
        spareCardListLength = deck.cardList.Count;//getting variables
        try//attempts to enable the next sibling if there is another card (after the one listed by this object) 
        {
            sibling(card.GetSiblingIndex() + 1).SetActive(intIfInDeck(inDeck)> card.GetSiblingIndex() + 1);
        }
        catch { }
        card.gameObject.SetActive(intIfInDeck(inDeck) > card.GetSiblingIndex());
        
        try
        {
            if (!inDeck)
            {
                cardName = deck.cardList[card.GetSiblingIndex()];
            }
            else
            {
                cardName = deck.tempCardList[card.GetSiblingIndex()];
            }
            gameObject.GetComponent<Text>().text = cardName;
        }
        catch{}
    }
    public void cardToggle()//making sure the correct cards display label things are enabled
    {
        if (transform.GetSiblingIndex() > 0) sibling(card.GetSiblingIndex()-1).SetActive(true);
        if (inDeck)
        {
            deck.cardList.Add(cardName);
            deck.tempCardList.RemoveAt(card.GetSiblingIndex());
            sibling(tempCardListLength).SetActive(false);
        }
        else
        {
            deck.tempCardList.Add(cardName);
            deck.cardList.RemoveAt(card.GetSiblingIndex());
            
            sibling(spareCardListLength).SetActive(false);
        }
        Otherlist.transform.GetChild(intIfInDeck(false) - 1).gameObject.SetActive(true);
        tempCardListLength = deck.tempCardList.Count;
        spareCardListLength = deck.cardList.Count;
        Otherlist.transform.GetChild(0).gameObject.SetActive(true);
    }
    void Update()
    {
        
        try
        {
            if(!sibling(card.GetSiblingIndex() - 1).activeInHierarchy)sibling(transform.GetSiblingIndex() - 1).SetActive(true);
        }
        catch
        { }
        try
        {
            if (inDeck != true)
            {
                cardName = deck.cardList[card.GetSiblingIndex()].ToString();
            }
            else
            {
                cardName = deck.tempCardList[card.GetSiblingIndex()];
            }
            gameObject.GetComponent<Text>().text = cardName;
        }
        catch{}
        if (tempCardListLength != deck.tempCardList.Count)
        {
            tempCardListLength= deck.tempCardList.Count;

        }
    }
}
