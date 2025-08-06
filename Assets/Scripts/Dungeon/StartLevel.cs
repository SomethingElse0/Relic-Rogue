using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerData playerData;
    public PlayerMovement player;
    public GameObject startLevel;
    public GameObject background;
    public GameObject noKeys;

    private void Awake()
    {
        playerData.coins += player.deck.coin * 0.25f;
        player.deck.coin=0;
    }
    // Update is called once per frame
    private void Interact()
    {
        player.actions.Disable();
        startLevel.SetActive(true);
        background.SetActive(true);
    }
    public void LevelStart()
    {
        if (playerData.keys < 1) noKeys.SetActive(true);
        else if (playerData.coins < 10) noKeys.SetActive(true);
        else 
        {
            playerData.keys--;
            playerData.coins -= 10;
            player.SendMessage("OnDisable");
            player.gameObject.SetActive(false);
            SceneManager.LoadScene(2); 
        }
        
    }
    public void LevelStart(int i)
    {
        SceneManager.LoadScene(i);
    }
}
