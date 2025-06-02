using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasure : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.parent.GetChild(0).SendMessage("GenerateSpecific", "coin");
        transform.parent.GetChild(0).SendMessage("GenerateSpecific", "levelKey");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
