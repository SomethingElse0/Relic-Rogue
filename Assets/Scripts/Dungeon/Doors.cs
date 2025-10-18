using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _object;
    public GameObject deactivate;
    public bool toHub;
    void Interact(Transform player)
    {

        if (player.GetComponent<PlayerMovement>().levelKey)
        {
            player.SendMessage("OpenDoor");
            _object.SetActive(_object != null);
            deactivate.SetActive(false);
            if (toHub == true) SceneManager.LoadScene(1);//seperate option for going back to the hub
        }
        
    }
}
