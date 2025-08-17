using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Paused : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<NavMeshAgent>().enabled = false;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        GetComponent<NavMeshAgent>().enabled = true;
    }
}
