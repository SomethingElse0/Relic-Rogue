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
    // Update is called once per frame
    private void Start()
    {
        HP = transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        Ammo = transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        HP.text="Health: "+player.GetComponent<PlayerMovement>().hp+" / 20";
        Ammo.text = "Ammo: " + weapon.GetComponent<WeaponSystem>().ammo;
    }
}
