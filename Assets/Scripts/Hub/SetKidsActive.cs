using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetKidsActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()=>transform.GetChild(0).gameObject.SetActive(true);
    //this is just to ensure that the cards are correctly listed

}
