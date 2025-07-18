using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    public Deck target;
    private void Awake()
    {
        target.generator.SendMessage("GenerateSpecific", "coin",SendMessageOptions.DontRequireReceiver);
        Destroy(this);   
    }
}
