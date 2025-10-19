using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{   
    public GenerateScrap generator;
    public  PlayerMovement player;
    private void Awake()
    {
        
        if(name.ToLower()!="levelkey")transform.rotation=generator.transform.GetChild(0).localRotation;//getting the correct rotation for objects
        //setting the position so that it will be reachable by the player
        else transform.rotation = generator.transform.GetChild(4).localRotation;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.3f);
    }
    private void Update()
    {
        if (transform.parent.childCount - 20 > transform.GetSiblingIndex())Destroy(gameObject,5);//sets a limit on the number of items that can be generated
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == player.gameObject)//ensuring that the player picks up the item
        {
            player.Pickup(name.ToLower());
            Destroy(gameObject);
        }
    }
}
