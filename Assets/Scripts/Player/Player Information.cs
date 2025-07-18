using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    // Start is called before the first frame update
    public int characterLV;
    public int maxPlayerHP;
    public int characterHPmult;
    public int basePlayerDEF;
    public int characterDEFmult;
    public int basePlayerATK;
    public int characterATKmult;
    public int inventorySlots;
    public int purchacedInventorySlots;
    public float playerSpeed;
    public System.DateTime LastPlayed;
}
