using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    Vector3 destination;

    // Update is called once per frame
    void FixedUpdate()
    {
        destination= new Vector3(player.position.x, player.position.y, transform.position.z);
        if ((transform.position - destination).magnitude < 0.006f) transform.position = destination;
        else transform.position = 0.97f * transform.position + 0.03f * destination;
    }
}
