using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateScrap : MonoBehaviour
{
    // These are the different items that can be generated over time
    
    List<GameObject> generateQueue = new List<GameObject>(); // The items selected to be created are stored in a list 
    GameObject scrap; // Scrap is used to buy cards in the scrapyard/reloading
    GameObject coin; // Coins are used to purchace items from
    GameObject ration; // Rations are used for healing
    GameObject fuel; // Fuel is used to travel to different dungeons
    GameObject levelKey; // Level Keys are used to access the next level
    // probability of an item being selected, the lower the number, the less likely to drop
    int scrapCount = 20;
    int fuelCount = 10;
    int coinCount = 5;
    int rationCount = 5;
    int levelKeyCount = 2;
    int colliders; // counter for the number of objects actively collided with
    float counters; // counter for the number of objects to be created
    bool isInLevel; // is the specified position in the level?
    float nextItemTime=10;
    float itemTimeInterval=0;
    int itemNumberCount=0;
    float cooldown = 0.3f;
    float lastAttempt;
    public Deck deck;
    public GameObject level;
    void Awake()
    {
        // identifying what each object is
        scrap = transform.GetChild(0).gameObject;
        scrap.SetActive(false);
        coin = transform.GetChild(1).gameObject;
        coin.SetActive(false);
        ration = transform.GetChild(2).gameObject;
        ration.SetActive(false);
        fuel = transform.GetChild(3).gameObject;
        fuel.SetActive(false);
        levelKey = transform.GetChild(4).gameObject;
        levelKey.SetActive(false);
        deck.generator = gameObject;

    }
    private void Update()
    {
        if (Time.fixedTime > itemTimeInterval)
        {
            RandomItem(itemNumberCount, 5);
            itemTimeInterval = Time.fixedTime + nextItemTime;
            itemNumberCount++;
        }
        if (colliders== 1&&Time.fixedTime>cooldown+lastAttempt&&isInLevel==false)
        {
            transform.position =level.transform.position+ new Vector3(Random.Range(-20,20), Random.Range(-20, 20), -0.8f);
            Ray newRay = new Ray(transform.position, new Vector3(0,0,1));
            if (Physics.Raycast(newRay, 5)&& colliders==0) isInLevel = true;
            else isInLevel = false;
            lastAttempt = Time.fixedTime;
            
        }
        
        if (isInLevel && generateQueue.Count > 0)
        {
            if (generateQueue[0] == null) generateQueue.RemoveAt(0);
            else 
            {
                GameObject newItem = Instantiate(generateQueue[0], transform.position, transform.rotation);
                generateQueue.RemoveAt(0);
                newItem.SetActive(true);
                isInLevel = false;
            }
        }
        
    }
    private void OnCollisionStay(Collision collision)
    {
            colliders=1;
    }
    private void OnCollisionExit(Collision collision)
    {
        colliders = 0;
    }

    public void RandomItem(int modifier, int counter)
    {
        counters += counter;
        int totalCount = scrapCount + coinCount + rationCount+fuelCount + modifier*2;
        totalCount -= Random.Range(0, totalCount) + scrapCount;
        while (counters > 0)
        {
            if (totalCount < 0)
            {
                GenerateSpecific(scrap);
                scrapCount--;
            }
            else
            {
                totalCount -= rationCount;
                if (totalCount < 0)
                {
                    generateQueue.Add(ration);
                    rationCount--;
                }
                else
                {
                    totalCount -= fuelCount;
                    if (totalCount < 0)
                    {
                        generateQueue.Add(fuel);
                        fuelCount--;
                    }
                    else
                    {
                        totalCount -= levelKeyCount;
                        if (totalCount < 0)
                        {
                            generateQueue.Add(coin);
                            coinCount--;
                        }
                        else
                        {
                            generateQueue.Add(levelKey);
                            levelKeyCount--;
                        }
                    }
                }
            }
            counters--;
        }
        scrapCount++;
        rationCount++;
        fuelCount++;
        coinCount++;
        GenerateSpecific(gameObject);
    }
    public void RandomItem(int counter)
    {
        counters += counter;
        int totalCount = coinCount + rationCount + fuelCount;
        totalCount -= Random.Range(0, totalCount + scrapCount);
        while (counters > 0)
        {
            if (totalCount < 0)
            {
                GenerateSpecific(scrap);
                scrapCount--;
            }
            else
            {
                totalCount -= rationCount;
                if (totalCount < 0)
                {
                    generateQueue.Add(ration);
                    rationCount--;
                }
                else
                {
                    totalCount -= fuelCount;
                    if (totalCount < 0)
                    {
                        generateQueue.Add(fuel);
                        fuelCount--;
                    }
                    else
                    {
                        totalCount -= levelKeyCount;
                        if (totalCount < 0)
                        {
                            generateQueue.Add(coin);
                            coinCount--;
                        }
                        else
                        {
                            generateQueue.Add(levelKey);
                            levelKeyCount--;
                        }
                    }
                }
            }
            counters--;
        }
        scrapCount++;
        rationCount++;
        fuelCount++;
        coinCount++;
        GenerateSpecific(gameObject);
    }
    public void GenerateSpecific(GameObject item)
    {
        if (item != gameObject) generateQueue.Add(item);
    }
    public void GenerateSpecific(string itemName)
    {
        
        if (itemName == "coin") generateQueue.Add(coin);
        else if (itemName == "scrap") generateQueue.Add(scrap);
        else if (itemName == "ration") generateQueue.Add(ration);
        else if (itemName == "fuel") generateQueue.Add(fuel);
        else if (itemName == "key") generateQueue.Add(levelKey);
    }
}