using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeckItem : MonoBehaviour
{
    public Deck deck;
    string cardName;
    List<string> spareCardList = new List<string>();
    public bool inDeck;
    public GameObject Otherlist;
    bool onStart = true;
    int tempCardListLength;
    int spareCardListLength;
    Transform card;
    GameObject sibling(int i)
    {
        return transform.parent.parent.GetChild(i).gameObject;
    }
    private void Awake()
    {
        card = transform.parent;
        tempCardListLength = deck.tempCardList.Count;
        spareCardListLength = deck.cardList.Count;
        if (inDeck)
        {
            try
            {
                sibling(card.GetSiblingIndex() + 1).SetActive(tempCardListLength> card.GetSiblingIndex() + 1);
            }
            catch { print(transform.parent.name); }
            if(tempCardListLength < card.GetSiblingIndex() + 1) card.gameObject.SetActive(false);

        }
        else
        {
            if (card.GetSiblingIndex() + 1 < card.parent.childCount)
            {
                sibling(card.GetSiblingIndex() + 1).SetActive(spareCardListLength > card.GetSiblingIndex()+1);
            }
            card.gameObject.SetActive(spareCardListLength > card.GetSiblingIndex());
        }
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
        catch
        {
        }
    }
    public void CardUpdate()
    {

    }
    public void cardToggle()
    {
        if (transform.GetSiblingIndex() > 0) sibling(card.GetSiblingIndex()-1).SetActive(true);
        if (inDeck)
        {
            deck.cardList.Add(cardName);
            deck.tempCardList.RemoveAt(card.GetSiblingIndex());
            tempCardListLength = deck.tempCardList.Count;
            spareCardListLength = deck.cardList.Count;
            print(tempCardListLength + " _ " + spareCardListLength);
            Otherlist.transform.GetChild(spareCardListLength-1).gameObject.SetActive(true);
            sibling(tempCardListLength).SetActive(false);
        }
        else
        {
            deck.tempCardList.Add(cardName);
            deck.cardList.RemoveAt(card.GetSiblingIndex());
            tempCardListLength=deck.tempCardList.Count;
            spareCardListLength=deck.cardList.Count;
            print(tempCardListLength + " _ " +spareCardListLength);
            Otherlist.transform.GetChild(tempCardListLength- 1).gameObject.SetActive(true);
            sibling(spareCardListLength).SetActive(false);
        }
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
        catch
        {
        }
        if (tempCardListLength != deck.tempCardList.Count)
        {
            tempCardListLength= deck.tempCardList.Count;
        }
    }
}
