using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treasure : MonoBehaviour
{
    // Start is called before the first frame update
    Transform generator;
    void Awake()
    {
        generator= transform.parent.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<GenerateScrap>().GenerateSpecific(transform.GetChild(1).gameObject);
        GetComponent<GenerateScrap>().GenerateSpecific(transform.GetChild(4).gameObject);
        Destroy(this);
    }
}
