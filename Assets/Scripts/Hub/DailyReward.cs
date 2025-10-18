using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    public PlayerData data;
    public bool dailyRewardAvailable;
    public GameObject dailyReward;
    public GameObject claimed;
    public GameObject background;
    void Awake()
    {
        dailyRewardAvailable=data.LastPlayed.Date != System.DateTime.Now.Date;
    }
    void Interact(Transform player)//this is called by the player
    {
        player.GetComponent<PlayerMovement>().OnDisable();
        if (dailyRewardAvailable) dailyReward.SetActive(true);
        else claimed.SetActive(true);
        background.SetActive(true);
        dailyReward.GetComponent<Button>().interactable = true;
    }
    public void Reward()//defines the reward, and a bonus for daily login streaks
    {
        if (data.LastPlayed.DayOfYear == System.DateTime.Now.DayOfYear - 1) data.keys += 2;
        data.LastPlayed = System.DateTime.Now;
        data.keys += 3;
        data.coins += Random.Range(12, 34);
        dailyRewardAvailable = false;
    }
}
