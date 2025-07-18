using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAuto : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    private void Awake()
    {
        target.atkSpeedMultiplier+=3;
        Destroy(this);   
    }
}
