using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePresent)
        {
            GetComponent<Light>().range = 10;
            transform.position = Camera.main.ScreenToWorldPoint(Vector3.Normalize(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)));
        }
        else GetComponent<Light>().range = 1000;

    }
}
