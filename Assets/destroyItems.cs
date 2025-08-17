using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyItems : MonoBehaviour
{
    // Start is called before the first frame update
    public GenerateScrap generator;
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "pickup")
        {
            generator.GenerateSpecific(col.transform.name);
            Destroy(col.gameObject);
        }
    }

    // Update is called once per frame
}
