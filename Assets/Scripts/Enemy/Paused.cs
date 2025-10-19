using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Paused : MonoBehaviour
{
    //pausing enemies temporarily
    void Awake()=>GetComponent<NavMeshAgent>().enabled = false;   
    private void OnDestroy()=>GetComponent<NavMeshAgent>().enabled = true;
    
}
