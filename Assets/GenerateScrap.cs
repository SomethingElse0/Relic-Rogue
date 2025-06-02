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
    float nextItemTime=20;
    float itemTimeInterval;
    int itemNumberCount;
    void Awake()
    {
        // identifying what each object is
        GameObject scrap = transform.GetChild(0).gameObject;
        GameObject coin = transform.GetChild(1).gameObject;
        GameObject ration = transform.GetChild(2).gameObject;
        GameObject fuel = transform.GetChild(3).gameObject;
        GameObject levelKey = transform.GetChild(4).gameObject;
        
    }
    private void Update()
    {
        if (Time.fixedTime > itemTimeInterval)
        {
            RandomItem(itemNumberCount, 3);
            itemTimeInterval = Time.fixedTime + nextItemTime;
            itemNumberCount++;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) isInLevel = true;
        else
        {
            colliders++;
            transform.position = new Vector3(Random.Range(0, 100), Random.Range(0, 100), 0);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) isInLevel = false;
        else
        {
            colliders--;
        }
    }
    public void RandomItem(int modifier, int counter)
    {
        counters += counter;
        int totalCount = scrapCount + coinCount + rationCount+fuelCount + modifier;
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
    public void GenerateSpecific(GameObject item)
    {
        if (item!=gameObject) generateQueue.Add(item);
        while (generateQueue.Count>0)
        {
            if (isInLevel && colliders == 0)
            {
                Instantiate(generateQueue[0], transform.position, transform.rotation);
                generateQueue.RemoveAt(0);
            }
        }
    }
}