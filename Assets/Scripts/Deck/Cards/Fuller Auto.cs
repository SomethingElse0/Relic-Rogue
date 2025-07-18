using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullerAuto : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    private void Awake()
    {
        target.atkSpeedMultiplier+=5;
        Destroy(this);   
    }
}
