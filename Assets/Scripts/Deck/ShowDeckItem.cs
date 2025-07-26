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
    private void Awake()
    {
        
        if (inDeck == true) 
        {
            if (transform.parent.GetSiblingIndex() + 1 < transform.parent.parent.childCount)
            {
                transform.parent.parent.GetChild(transform.parent.GetSiblingIndex() + 1).gameObject.SetActive(deck.cardList.Count - deck.tempCardList.Count !< transform.parent.GetSiblingIndex() + 1);
            }
            transform.parent.gameObject.SetActive(deck.cardList.Count - deck.tempCardList.Count !< transform.parent.GetSiblingIndex());
        }
        else
        {
            if (transform.parent.GetSiblingIndex() + 1 < transform.parent.parent.childCount)
            {
                transform.parent.parent.GetChild(transform.parent.GetSiblingIndex() + 1).gameObject.SetActive(deck.tempCardList.Count !< transform.parent.GetSiblingIndex()+1);
            }
            transform.parent.gameObject.SetActive(deck.tempCardList.Count !< transform.parent.GetSiblingIndex());
        }
        spareCardList.Clear();
        spareCardList.AddRange(deck.cardList);
        foreach (string i in deck.tempCardList)
        {
            spareCardList.Remove(i);
        }
    }
    public void cardToggle()
    {
        if (inDeck == false)
        {
            deck.tempCardList.Add(cardName);
            spareCardList.RemoveAt(transform.parent.GetSiblingIndex());
            Otherlist.transform.GetChild(deck.tempCardList.Count-1).gameObject.SetActive(true);
        }
        else
        {
            spareCardList.Add(cardName);
            deck.tempCardList.RemoveAt(transform.parent.GetSiblingIndex());
            Otherlist.transform.GetChild(spareCardList.Count - 1).gameObject.SetActive(true);
        }
    }
    void Update()
    {
        try
        {
            if (inDeck != true)
            {
                cardName = spareCardList[transform.parent.GetSiblingIndex()];
            }
            else
            {
                cardName = deck.tempCardList[transform.parent.GetSiblingIndex()];
            }
            gameObject.GetComponent<Text>().text = cardName;
        }
        catch
        {
            transform.parent.gameObject.SetActive(false);
        }
        
    }
}
