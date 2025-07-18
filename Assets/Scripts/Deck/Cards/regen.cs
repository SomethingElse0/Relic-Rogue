using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regen : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    float endTime=-1;
    private void Awake()
    {
        target.hpRegenTemp+=5;
        endTime = Time.fixedTime + 30f;
    }

}
