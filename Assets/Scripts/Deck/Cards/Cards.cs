using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    float endTime=-1f;
    void Alarmed()
    {
        target.enemyAwareness++;
    }
    void Rubble()
    {
        target.hazardProb += 2;
    }
    void Coin()
    {
        target.generator.SendMessage("GenerateSpecific", "coin", SendMessageOptions.DontRequireReceiver);
    }
    void FalseCollapse()
    {
        target.hazardProb = Mathf.Abs(target.hazardProb);
        target.hazardProb += 3;
        target.deck.GetComponent<DeckRandomiser>().numberOfCards += 2;
        target.deck.GetComponent<DeckRandomiser>().CardSelect();
    }
    void Part2()
    {
        target.hazardProb = -Mathf.Abs(target.hazardProb);
    }
    void Focus()
    {
        target.atkSpeedMultiplier = target.atkSpeedMultiplier * 0.9f;
        target.playerDamage += 3;
        Destroy(this);
    }
    void FullAuto()
    {
        target.atkSpeedMultiplier += 3;
    }
    void FullerAuto()
    {
        target.atkSpeedMultiplier += 5;
    }
    void IntoTheDepths()
    {
        target.generator.GetComponent<GenerateScrap>().GenerateSpecific("key");
        target.tempCardList.RemoveAt(1);
        Rubble();
    }
    void Resourceful()
    {
        if (target.tempCardList[0] == "Resouceful") GetComponent<DeckRandomiser>().AddToDeck(target.tempCardList, target.tempCardList[1]);
        GetComponent<DeckRandomiser>().AddToDeck(target.tempCardList, target.tempCardList[0]);
    }
    void hpRegenTemp()
    {
        target.hpRegenTemp += 5;
        endTime = Time.fixedTime + 30f;
    }
    void Update()
    {
        if (Time.fixedTime > endTime && endTime > 0)
        {
            target.hpRegenTemp -= 5;
            if (target.hpRegenTemp > 0) target.hpRegenTemp = 0;
            endTime = -1;
        }
    }
    void Scrapped()
    {
        target.scrapTemp += 3;
        target.generator.SendMessage("GenerateScrap", SendMessageOptions.DontRequireReceiver);

    }
    void Protection()
    {
        target.hpTemp += 5;
    }
    void SecondChance()
    {
        target.hpTemp += 30;
        target.enemyAwareness += 3;
    }
}
