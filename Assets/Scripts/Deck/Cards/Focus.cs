using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    private void Awake()
    {
        target.atkSpeedMultiplier=target.atkSpeedMultiplier*0.9f;
        target.playerDamage+=3;
        Destroy(this);   
    }
}
