using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    // Start is called before the first frame update
    public int maxPlayerHP;
    public int basePlayerDEF;
    public int basePlayerATK;
    public float playerSpeed;
    public System.DateTime LastPlayed;
    public int coins;
    public int keys;
}
