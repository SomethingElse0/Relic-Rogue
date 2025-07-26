using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerData playerData;
    void OnStartLevel()
    {
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player"&& playerData.keys>0) 
        {
            playerData.keys--; 
            OnStartLevel(); 
        }
    }
}
