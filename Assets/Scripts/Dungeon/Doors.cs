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
            try { _object.SetActive(true); }
            catch { SceneManager.LoadScene(1); }
            deactivate.SetActive(false);
             //seperate option for going back to the hub
        }
        
    }
}
