using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeckItem : MonoBehaviour
{
    public Deck deck;
    string cardName;
    public bool inDeck;
    public GameObject deckParent;
    public GameObject spareParent;
    
    Transform card;
    int intIfInDeck(bool b)// I'm using this to simplify the logic, so that I don't have to write the code twice.
    {
        if (b) return deck.tempCardList.Count;
        else return deck.cardList.Count;
    }
    List<string> cardListIfInDeck(bool b)// I'm using this to simplify the logic, so that I don't have to write the code twice.
    {
        if (b) return deck.tempCardList;
        else return deck.cardList;
    }
    GameObject parentListIfInDeck(bool b)// I'm using this to simplify the logic, so that I don't have to write the code twice.
    {
        if (b) return deckParent;
        else return spareParent;
    }
    GameObject sibling(int i)
    {
        return transform.parent.parent.GetChild(i).gameObject;
    }
    private void Awake()
    {
        card = transform.parent;//getting variables 
        //sibling(intIfInDeck(inDeck)-1).SetActive(true);//
        //if(card.GetSiblingIndex()>0)sibling(card.GetSiblingIndex() - 1).SetActive(true);
        try
        {
            deckParent = sibling(0).transform.GetChild(0).GetComponent<ShowDeckItem>().deckParent;
            spareParent = sibling(0).transform.GetChild(0).GetComponent<ShowDeckItem>().spareParent;
        }
        catch { }
        card.GetComponent <RectTransform>().localPosition=new Vector3(-200 , 225-(card.GetSiblingIndex()*25), 0);
        if(card.GetSiblingIndex() < intIfInDeck(inDeck))Instantiate(card, card.position+25*Vector3.down,card.rotation, card.transform.parent);
        //card.gameObject.SetActive(intIfInDeck(inDeck) > card.GetSiblingIndex())        
        try
        {
            if (!inDeck) cardName = deck.cardList[card.GetSiblingIndex()];
            else cardName = deck.tempCardList[card.GetSiblingIndex()];
            gameObject.GetComponent<Text>().text = cardName;
        }
        catch{ card.gameObject.SetActive(false); 
        }
    }

    public void cardToggle()//making sure the correct cards display label things are enabled
    {
        //if (card.GetSiblingIndex() > 0) 
        //{ 
        //    sibling(card.GetSiblingIndex() - 1).SetActive(true);
        //}
        try
        {
            cardListIfInDeck(!inDeck).Add(deck.cardList[card.GetSiblingIndex()]);
            cardListIfInDeck(inDeck).RemoveAt(card.GetSiblingIndex());

        }
        catch
        {
            
        }
        card.SetParent(parentListIfInDeck(!inDeck).transform);
        inDeck = !inDeck;
        try { if (!sibling(card.GetSiblingIndex() - 1).activeInHierarchy)Destroy(sibling(card.GetSiblingIndex() - 1)); } catch { }
        //otherList.transform.GetChild(intIfInDeck(!inDeck)).gameObject.SetActive(true)
        //sibling(intIfInDeck(inDeck)).SetActive(false);
    }
    private void Update()
    {
        card.GetComponent<RectTransform>().localPosition = new Vector3(91.5f, - (card.GetSiblingIndex()+1) * 25, 0);
    }
    //public void CardToggle()//only need to update in one direction 
    //{
    //    try
    //    {
    //        if (card.GetSiblingIndex()+1<intIfInDeck(inDeck)) sibling(card.GetSiblingIndex() + 1).SetActive(true);
    //    }
    //    catch { }
    //    try
    //    {
    //        sibling(card.GetSiblingIndex() + 1).transform.GetChild(0).GetComponent<ShowDeckItem>().CardToggle();
    //    }
    //    catch { }
    //    if (inDeck) cardName = deck.tempCardList[card.GetSiblingIndex()];
    //    else cardName = deck.cardList[card.GetSiblingIndex()];
    //    gameObject.GetComponent<Text>().text = cardName;
    //    
    //}
    //void DisabledFix()
    //{
    //    if (sibling(card.GetSiblingIndex() + 1).activeInHierarchy)
    //    {
    //        sibling(card.GetSiblingIndex() + 1).SetActive(false)
    //         card.gameObject.SetActive(true)
    //    }
    //}

}
