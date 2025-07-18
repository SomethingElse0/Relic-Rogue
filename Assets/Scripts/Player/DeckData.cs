using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DeckData", menuName = "ScriptableObjects/DeckData", order = 1)]
public class Deck : ScriptableObject
{
    public GameObject player;
    public GameObject weapon;
    public GameObject generator;
    public GameObject deck;
    public List<string> cardList;//list of cards in deck to begin with
    public List<string> tempCardList;//list of remaining cards in the active deck
    public List<string> allCards;//list of all cards that exist
    public List<string> trapCards;//list of trap cards
    public float hpTemp;//player temporary hp - damage first applies to temp hp. Cannot be regenerated
    public int damageModifier;
    public int hpRegenTemp;//rate at which regenerates hp
    public int playerDamage;//deals damage to rhe player
    public int scrapTemp;//increases the quality of items generated
    public int hazardProb;//probability that hazards will activate, closing off some paths, and opening paths for enemies
    public float atkSpeedMultiplier;//affects the rate at which attacs come out
    public int enemyAwareness;//probability enemies will attempt to pathfind to you when not in line of sight/not able to pathfind to you
    
}
