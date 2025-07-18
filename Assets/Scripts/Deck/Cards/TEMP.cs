using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    private void Awake()
    {
        target.hazardProb++;
        Destroy(this);   
    }
}
