using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    Scene menu;
    Scene hub;
    float keybindTime;
    Transform canvas;
    InputActionAsset actions;
    int menuSceneCurrent;
    void SceneChangeHub()
    {
        SceneManager.LoadScene(hub.buildIndex);
        
    }
    void SceneChangeMenu()
    {
        SceneManager.LoadScene(menu.buildIndex);
    }
    public void MenuChange(int i)
    {
        canvas.GetChild(i).gameObject.SetActive(true);
        canvas.GetChild(menuSceneCurrent).gameObject.SetActive(false);
        menuSceneCurrent = i;
    }
    void updateKeybinds(InputAction action)
    {
        keybindTime = Time.fixedTime + 10;
        while (keybindTime > Time.fixedTime) 
        {
            if (Input.anyKeyDown) 
            {   
                actions.FindActionMap("Movement").FindAction(action.name).ApplyBindingOverride(Keyboard.current.quoteKey.name);
                keybindTime = 0;
            }
        }
    }
    // Update is called once per frame
}
