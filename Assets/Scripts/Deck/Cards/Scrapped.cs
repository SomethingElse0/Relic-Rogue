using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrapped : MonoBehaviour
{
    // Rejected alternate names: hopes and dreams / my vision of the game.
    public Deck target;
    private void Awake()
    {
        target.scrapTemp += 3;
        target.generator.SendMessage("GenerateScrap", SendMessageOptions.DontRequireReceiver);
        
    }
}
