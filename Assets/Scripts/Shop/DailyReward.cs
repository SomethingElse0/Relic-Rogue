using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyReward : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerData data;
    bool dailyRewardAvailable;
    void Awake()
    {
        if (data.LastPlayed.Date != System.DateTime.Now.Date) dailyRewardAvailable=true;
    }
    void Interact()
    {
        data.LastPlayed = System.DateTime.Now;
        data.keys++;
        data.coins += Random.Range(12, 34);
        dailyRewardAvailable = false;
    }
}
