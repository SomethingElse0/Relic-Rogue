using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

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
    //bool isInLevel; // is the specified position in the level?
    float nextItemTime=9;
    float itemTimeInterval=5;
    int itemNumberCount=0;
    float cooldown = 0.5f;
    float lastAttempt;
    public Deck deck;
    public GameObject level;
    public List<Vector3> positions = new List<Vector3>();
    NavMeshAgent agent;
    Quaternion rotation;
    Quaternion keyRotation;
    public Vector3 destination;
    Vector3 levelScale;
    Transform itemsHere;
    void Awake()
    {
        // identifying what each object is
        levelScale = transform.parent.GetChild(0).GetComponent<NavMeshObstacle>().size; //so that the items are generated around the level, and not outside/limited to only one area
        itemsHere = transform.parent.GetChild(transform.GetSiblingIndex() - 1);
        scrap = transform.GetChild(0).gameObject;
        coin = transform.GetChild(1).gameObject;
        ration = transform.GetChild(2).gameObject;
        fuel = transform.GetChild(3).gameObject;
        levelKey = transform.GetChild(4).gameObject;
        deck.generator = gameObject;
        rotation = scrap.transform.rotation;
        rotation = levelKey.transform.rotation;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        positions.Add(transform.position);//addsthe start position as a possible item location
    }
    private void Update()
    {
        
        if (Time.fixedTime > itemTimeInterval)//regularly spawns in 3 items
        {
            RandomItem(itemNumberCount, 3);
            itemTimeInterval = Time.fixedTime + nextItemTime;
            itemNumberCount++;
        }
        if (Time.fixedTime > cooldown + lastAttempt && agent.velocity.magnitude < 1 && positions.Count < 20)
        {
            bool inList = false;
            if (positions.Count > 0 && positions.Count < 20)
            {
                if (positions.Contains(new Vector3(agent.pathEndPosition.x, agent.pathEndPosition.x, -0.05f))) inList = true;
            }
            if (inList == false) positions.Add(new Vector3(transform.position.x, transform.position.y, -0.05f));
            destination = level.transform.position + new Vector3(Random.Range(-0.5f * levelScale.x, 0.5f * levelScale.x), Random.Range(-0.5f * levelScale.y, 0.5f * levelScale.y), transform.position.z);
            agent.SetDestination(destination);
            lastAttempt = Time.fixedTime;
        }
        else if (positions.Count > 20 && (agent.transform.position-agent.pathEndPosition).magnitude<0.1f) destination = positions[Random.Range(0,positions.Count)];//selects a random destination
        if (generateQueue.Count > 0)
        {
            if (generateQueue[0] != null)
            {
                
                int randomPositionNo = Random.Range(0, positions.Count - 1);
                if (positions.Count > 3)
                { 
                    
                    GameObject newItem = Instantiate(generateQueue[0], positions[randomPositionNo], rotation);
                    newItem.SetActive(true);
                    newItem.transform.SetParent(itemsHere);
                    if (newItem.name.ToLower() == "levelkey(clone)") newItem.transform.rotation = keyRotation;
                    if (Random.Range(-3, positions.Count) > randomPositionNo) positions.RemoveAt(randomPositionNo);
                    lastAttempt--;
                }
                else if (agent.velocity.magnitude == 0)
                {
                    destination = transform.position;
                    GameObject newItem = Instantiate(generateQueue[0], new Vector3(destination.x, destination.y, -0.05f), generateQueue[0].transform.rotation);
                    newItem.SetActive(true);
                    if (newItem.name.ToLower() == "levelkey(clone)") newItem.transform.rotation = keyRotation;
                    newItem.transform.SetParent(itemsHere);
                    lastAttempt--;
                }
            }
            generateQueue.RemoveAt(0);
        }
        
    }
    public void RandomItem(int modifier, int counter)//picking a random item to be generated
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
    public void RandomItem(int counter)//same as above, but with only an int counter for the number of items to be generated
    {
        counters += counter;
        int totalCount = coinCount + rationCount + fuelCount;
        totalCount -= Random.Range(0, totalCount + scrapCount);
        while (counters > 0)
        {
            if (totalCount < 0)
            {
                GenerateSpecific(scrap);
                scrapCount -= 3;
            }
            else
            {
                totalCount -= rationCount;
                if (totalCount < 0)
                {
                    generateQueue.Add(ration);
                    rationCount -= 3;
                }
                else
                {
                    totalCount -= fuelCount;
                    if (totalCount < 0)
                    {
                        generateQueue.Add(fuel);
                        fuelCount -= 3;
                    }
                    else
                    {
                        totalCount -= levelKeyCount;
                        if (totalCount > 0)
                        {
                            generateQueue.Add(coin);
                            coinCount-=3;
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
        else if (itemName == "levelkey") generateQueue.Add(levelKey);
    }
}


