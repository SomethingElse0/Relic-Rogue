using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIupdate : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform weapon;
    TMPro.TextMeshProUGUI HP;
    TMPro.TextMeshProUGUI Ammo;
    TMPro.TextMeshProUGUI Fuel;
    TMPro.TextMeshProUGUI keyCard;
    // Update is called once per frame
    private void Awake()
    {
        HP = transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        Ammo = transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        Fuel = transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();
        keyCard = transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>();
        
    }

    void Update()
    {
        HP.text="Health: "+player.GetComponent<PlayerMovement>().hp+" / 20";
        Ammo.text = "Ammo: " + weapon.GetComponent<WeaponSystem>().ammo;
        Fuel.text = "Fuel: " + player.GetComponent<PlayerMovement>().deck.fuel;
        keyCard.text = "Key Card: " + player.GetComponent<PlayerMovement>().levelKey.ToString();
    }
}
