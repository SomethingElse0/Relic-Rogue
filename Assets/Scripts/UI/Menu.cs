using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Scene menu;
    public Scene hub;
    float keybindTime=5;
    public Transform canvas;
    public InputActionAsset actions;
    int menuSceneCurrent;
    int arbitraryNumber;
    string thisKey;
    string savedAction;
    bool waitingforInput;
    InputAction targetAction;
    string newPath;
    private void Awake()
    {
        Keyboard.current.onTextInput += cha => thisKey=cha.ToString();
        menu = SceneManager.GetSceneByName("Menu");
        hub = SceneManager.GetSceneByName("hub");
    }
    private void Update()
    {
        if (waitingforInput == true)
        {
            
            
            
            if (Input.anyKeyDown) 
            {
                print(newPath + thisKey);
                newPath = newPath.Remove(newPath.LastIndexOf('/') + 1);
                UpdateKeybinds(savedAction, newPath + thisKey);
                print(newPath + thisKey);
            }
        }
    }
    public void SceneChangeHub()
    {
        SceneManager.LoadScene(1);
    }
    public void SceneChangeMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void MenuChange(int i)
    {
        canvas.GetChild(i).gameObject.SetActive(true);
        canvas.GetChild(menuSceneCurrent).gameObject.SetActive(false);
        menuSceneCurrent = i;
    }
    
    public void UpdateKeybinds(string action)
    {
        try 
        {
            targetAction = actions.FindActionMap("Movement").FindAction(action);
            newPath = targetAction.bindings[0].path;
        }
        catch
        {
            targetAction = actions.FindActionMap("Movement").FindAction("movement");
            newPath = targetAction.bindings[1].path;
        }
        savedAction = action;
        waitingforInput = true;
        
    }
    public void UpdateKeybinds(string action, string new_Path)
    {
        if (action == "Up") targetAction.ApplyBindingOverride(1, new_Path);
        else if (action == "Down") targetAction.ApplyBindingOverride(2, new_Path);
        else if (action == "Left") targetAction.ApplyBindingOverride(3, new_Path);
        else if (action == "Right") targetAction.ApplyBindingOverride(4, new_Path);
        else targetAction.ApplyBindingOverride(0, new_Path);
        canvas.GetChild(1).GetComponent<ShowControlls>().UpdateControls();
        waitingforInput = false;
    }
    // Update is called once per frame
}
