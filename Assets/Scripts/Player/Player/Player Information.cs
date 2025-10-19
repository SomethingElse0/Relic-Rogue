using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    // Start is called before the first frame update
    public int maxPlayerHP;//this sets the maximum hp the player can have
    public float playerSpeed;//the speed at which the player  moves at
    public System.DateTime LastPlayed;//This is used to track whether the daily reward is available 
    public float coins;//stores the number of coins the player has
    public int keys;//stores the number of keys the player has, and therefore the number of attempts
}
