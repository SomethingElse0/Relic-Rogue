using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class displayVar : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    TMPro.TextMeshProUGUI display;
    string startText;
    // Update is called once per frame
    private void Awake()
    {
        display = transform.GetComponent<TMPro.TextMeshProUGUI>();
        startText = display.text;
    }

    void Update()
    {
        if(this.name=="Coins")display.text = startText + player.GetComponent<PlayerMovement>().playerData.coins;
        else if(this.name=="Keys")display.text = startText + player.GetComponent<PlayerMovement>().playerData.keys;
    }
}
