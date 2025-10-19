using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform menu;
    public Transform player;
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    private void Interact()
    {
        menu.gameObject.SetActive(true);
        player.SendMessage("OnDisable");
    }

}
