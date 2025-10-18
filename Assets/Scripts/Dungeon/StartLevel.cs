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
    public GameObject noCoins;
    private void Interact()//disables the player during the interaction, preventing various issues
    {
        player.actions.Disable();
        startLevel.SetActive(true);
        background.SetActive(true);
    }
    public void LevelStart()//this function checks whether the player can start the level
    {
        if (playerData.keys < 1) noKeys.SetActive(true);
        else if (playerData.coins < 10) noCoins.SetActive(true);
        else 
        {
            playerData.keys--;//removes the cost of a new level from what the player has
            playerData.coins -= 10;
            SceneManager.LoadScene(2); 
        }
        
    }
    public void LevelStart(int i)=>SceneManager.LoadScene(i);//simple scene switch
    
}
