using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    float endTime=-1f;
    float hp;
    PlayerMovement player;
    private void Awake()
    {
        hp = target.player.GetComponent<PlayerMovement>().hp;
        player = target.player.GetComponent<PlayerMovement>();
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
        //temporarily increases the probability of a hazard and plays the next two cards sooner
    }
    void Part2()=>target.hazardProb = -Mathf.Abs(target.hazardProb);//false collapse part 2: increases the stability.

    void Focus()
    {
        target.atkSpeedMultiplier = target.atkSpeedMultiplier * 0.9f;
        target.playerDamage ++;
    }//increases the ammount of damage, and the attack rate
    void FullAuto()=>target.atkSpeedMultiplier += 3;//increases the player attack speed
    
    void FullerAuto()=>target.atkSpeedMultiplier += 5;//increases the player attack speed

    void IntoTheDepths()//generates a key, but removes the next card in the deck
    {
        target.generator.GetComponent<GenerateScrap>().GenerateSpecific("key");
        target.tempCardList.RemoveAt(1);
        Rubble();
    }
    void Resourceful()//adds a copy of the next card to the deck
    {
        if (target.tempCardList[1] == "Resouceful") GetComponent<DeckRandomiser>().AddToDeck(target.tempCardList, target.tempCardList[1]);
        GetComponent<DeckRandomiser>().AddToDeck(target.tempCardList, target.tempCardList[1]);
    }
    void hpRegenTemp()//regenertes  hp
    {
        target.hpRegenTemp += 5;
        endTime = Time.fixedTime + 20f;
    }
    
    void Update()
    {
        if (Time.fixedTime > endTime && endTime > 0&&hp<player.playerData.maxPlayerHP)
        {
            hp++;
            target.hpRegenTemp--;
            if (target.hpRegenTemp > 0) endTime += 5;
            else endTime = -1;
        }
    }
    void Scrapped()//generates an item, and increases the probability of scrap
    {
        target.scrapTemp += 3;
        target.generator.SendMessage("GenerateScrap", SendMessageOptions.DontRequireReceiver);

    }
    void Protection()=>target.hpTemp += 5;//gives 5 temp hp
    
    void SecondChance()//gives a lot of bonus hp, but increases enemy awareness
    {
        target.hpTemp += 30;
        target.enemyAwareness += 5;
    }
}
