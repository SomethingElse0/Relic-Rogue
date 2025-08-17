using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIupdate : MonoBehaviour
{
    // Start is called before the first frame update
    Transform weapon;
    public Transform player;
    PlayerMovement playerMovement;
    TMPro.TextMeshProUGUI hp;
    TMPro.TextMeshProUGUI ammo;
    TMPro.TextMeshProUGUI fuel;
    TMPro.TextMeshProUGUI keyCard;
    TMPro.TextMeshProUGUI scrap;
    TMPro.TextMeshProUGUI rations;
    TMPro.TextMeshProUGUI coin;
    TMPro.TextMeshProUGUI lastCard;
    // Update is called once per frame
    private void Awake()
    {
        hp = transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        ammo = transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        fuel = transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();
        keyCard = transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>();
        scrap = transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>();
        rations = transform.GetChild(6).GetComponent<TMPro.TextMeshProUGUI>();
        coin = transform.GetChild(7).GetComponent<TMPro.TextMeshProUGUI>();
        lastCard = transform.GetChild(8).GetComponent<TMPro.TextMeshProUGUI>();
        playerMovement = player.GetComponent<PlayerMovement>();
        weapon = playerMovement.weapon.transform;
    }

    void Update()
    {
        hp.text="Health: "+playerMovement.hp+" / 20";
        ammo.text = "Ammo: " + weapon.GetComponent<WeaponSystem>().ammo;
        fuel.text = "Fuel: " + playerMovement.deck.fuel;
        keyCard.text = "Key Card: " + playerMovement.levelKey.ToString();
        scrap.text = "Scrap: " + playerMovement.deck.scrap;
        coin.text = "Coins: " + playerMovement.deck.coin;
        rations.text = "Rations: " + playerMovement.deck.rations;
        lastCard.text = "Last Card: " + playerMovement.deck.deck.GetComponent<DeckRandomiser>().lastCard;
    }
}
