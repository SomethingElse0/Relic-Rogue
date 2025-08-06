using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerData data;
    public bool dailyRewardAvailable;
    public GameObject dailyReward;
    public GameObject claimed;
    public GameObject background;
    void Awake()
    {
        dailyRewardAvailable=(data.LastPlayed.Date != System.DateTime.Now.Date);
    }
    void Interact(Transform player)
    {
        player.GetComponent<PlayerMovement>().actions.Disable();
        if (dailyRewardAvailable) dailyReward.SetActive(true);
        else claimed.SetActive(true);
        background.SetActive(true);
        dailyReward.GetComponent<Button>().interactable = true;
    }
    public void Reward()
    {
        data.LastPlayed = System.DateTime.Now;
        data.keys += 6;
        data.coins += Random.Range(12, 34);
        dailyRewardAvailable = false;
    }
}
