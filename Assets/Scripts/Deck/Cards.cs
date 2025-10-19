using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    float endTime = -1f;
    float hp;
    int regenLimit = -5;
    
    PlayerMovement player;
    List<string> activeCards = new List<string>();
    List<float> cardEndTime = new List<float>();
    private void Awake()
    {
        hp = target.player.GetComponent<PlayerMovement>().hp;
        player = target.player.GetComponent<PlayerMovement>();
    }
    void SetTimeLimit(string functionName, float endTime)
    {
        int counter = 0;
        foreach (int i in cardEndTime) { if (cardEndTime[i] >= endTime) counter = i; } 
        cardEndTime.Insert(counter, endTime);  
    }
    void Alarmed()=>target.enemyAwareness++;//increases how aggressive the enemy is - not fully implemented
    
    void Rubble()=>target.hazardProb += 2;//increases how likely a hazard is to trigger
    
    void Coin()=>target.generator.SendMessage("GenerateSpecific", "coin", SendMessageOptions.DontRequireReceiver);//generates a coin somewhere
    
    void FalseCollapse()
    {
        target.hazardProb = Mathf.Abs(target.hazardProb);
        target.hazardProb += 3;
        target.deck.GetComponent<DeckRandomiser>().numberOfCards += 2;
        target.deck.GetComponent<DeckRandomiser>().CardSelect();
        SetTimeLimit("Part2",Time.fixedTime+10);
        //temporarily increases the probability of a hazard and plays the next two cards sooner
    }
    void Part2()
    {
        target.hazardProb = -Mathf.Abs(target.hazardProb);//false collapse part 2: increases the stability.
    }
    void Focus()
    {
        target.atkSpeedMultiplier = target.atkSpeedMultiplier * 0.9f;
        target.playerDamage ++;
        SetTimeLimit("FocusEnd", 20 + Time.fixedTime);
    }//increases the ammount of damage, and the attack rate
    void FocusEnd()
    {
        target.atkSpeedMultiplier = target.atkSpeedMultiplier / 0.9f;
        target.playerDamage--;
    }
    void FullAuto() 
    { 
        target.atkSpeedMultiplier += 3;
        SetTimeLimit("FullAutoEnd", 16 + Time.fixedTime);
    }//increases the player attack speed
    void FullAutoEnd()
    {
        target.atkSpeedMultiplier -= 3;
    }//increases the player attack speed

    void FullerAuto()
    {
        target.atkSpeedMultiplier += 5;//increases the player attack speed
    }
    void FullerAutoEnd()
    {
        target.atkSpeedMultiplier -= 5;//increases the player attack speed
    }
    void IntoTheDepths()//generates a key, but removes the next card in the deck
    {
        target.generator.GetComponent<GenerateScrap>().GenerateSpecific("key");
        target.tempCardList.RemoveAt(1);
        Rubble();
    }
    void Resourceful()//adds two coppies of the next card to the deck, unless it is another "resourceful" 
    {
        if (target.tempCardList[1] != "Resouceful") GetComponent<DeckRandomiser>().AddToDeck(target.tempCardList, target.tempCardList[1]);
        GetComponent<DeckRandomiser>().AddToDeck(target.tempCardList, target.tempCardList[1]);
    }
    void hpRegenTemp()//regenertes  hp
    {
        target.hpRegenTemp += 5;
        regenLimit += 5;
        SetTimeLimit("hpRegenTempEnd", Time.fixedTime + 20f);
    }
    
    void Update()
    {
        if (Time.fixedTime < cardEndTime[0])
        {
            SendMessage(activeCards[0]);
        }
        if (Time.fixedTime > endTime && endTime > 0&&hp<player.playerData.maxPlayerHP)
        {
            hp++;
            target.hpRegenTemp--;
            if (target.hpRegenTemp > 0) endTime += 5;
            else endTime = -1;
        }
        if (target.hpRegenTemp == regenLimit)
        {
            int i = activeCards.IndexOf("hpRegenTempEnd");
            regenLimit -= 5;
            activeCards.RemoveAt(i);
            cardEndTime.RemoveAt(i);
        }
    }
    void Scrapped()//generates an item, and increases the probability of scrap
    {
        target.scrapTemp += 3;
        target.generator.SendMessage("GenerateScrap", SendMessageOptions.DontRequireReceiver);

    }
    void Protection()=>target.hpTemp += 5;//gives 5 temp hp
    void ProtectionEnd() => target.hpTemp -= Mathf.Repeat(target.hpTemp, 5);

    void SecondChance()//gives a lot of bonus hp, but increases enemy awareness
    {
        target.hpTemp += 30;
        target.enemyAwareness += 5;
    }
}
