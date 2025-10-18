using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class displayVar : MonoBehaviour
{
    public Transform player;
    TMPro.TextMeshProUGUI display;
    string startText;
    
    private void Awake()
    {
        display = transform.GetComponent<TMPro.TextMeshProUGUI>();
        startText = display.text;
    }
    // Update is called once per frame
    void Update()
    {
        //this is ment to display specific stats - This script is for the hub area
        if(this.name=="Coins")display.text = startText + player.GetComponent<PlayerMovement>().playerData.coins;
        else if(this.name=="Keys")display.text = startText + player.GetComponent<PlayerMovement>().playerData.keys;
    }
}
