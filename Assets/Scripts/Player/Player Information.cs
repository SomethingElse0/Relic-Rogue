using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    // Start is called before the first frame update
    int characterLV;
    int maxPlayerHP;
    int characterHPmult;
    int basePlayerDEF;
    int characterDEFmult;
    int basePlayerATK;
    int characterATKmult;
    int inventorySlots;
    int purchacedInventorySlots;
    float playerSpeed = 4.5f;
    private void Start()
    {
        maxPlayerHP = 4 + characterLV * characterHPmult;
        inventorySlots = characterLV + purchacedInventorySlots;

    }
}
