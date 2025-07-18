using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _object;
    void Interact(Transform player)
    {

        player.SendMessage("OpenDoor");
        if (_object != gameObject) _object.SetActive(true);
        gameObject.SetActive(false);
    }
}
