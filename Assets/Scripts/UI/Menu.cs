using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    float bindingEndTime;
    float keybindTime=5;
    public Transform canvas;
    public InputActionAsset actions;
    int menuSceneCurrent;
    string thisKey;
    string savedAction;
    InputAction targetAction;
    string newPath;
    private void Awake() => Keyboard.current.onTextInput += cha => thisKey+=cha.ToString();//getting the last input
    private void Update()
    {
        if (actions!=null)
        {
            if (bindingEndTime < Time.fixedTime)
            {

                if (Input.anyKeyDown)
                {
                    if (thisKey == " ") thisKey = "Space";//getting the last input, and translating the space key
                    newPath = newPath.Remove(newPath.LastIndexOf('/') + 1) + thisKey;//translating into a path
                    CompleteUpdateKeybinds(savedAction, newPath);
                }
                else thisKey = "";
            }
            else thisKey = "";//to prevent characters pressed before changing keybinds from being included
        }
    }
    public void SceneChangeHub() => SceneManager.LoadScene(1);//quick functions for changing to specific scenes
    public void SceneChangeMenu() => SceneManager.LoadScene(0);//quick functions for changing to specific scenes

    public void MenuChange(int i)//changing menu scenes
    {
        canvas.GetChild(i).gameObject.SetActive(true);
        canvas.GetChild(menuSceneCurrent).gameObject.SetActive(false);
        menuSceneCurrent = i;
    }
    
    public void UpdateKeybinds(string action)//this is part 1, identifying which keybinds are being updated
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
        bindingEndTime = Time.fixedTime + keybindTime;
    }
    public void CompleteUpdateKeybinds(string action, string new_Path)//appplying the bindings
    {
        //movement directions are handled seperately due to being a composite binding
        if (action == "Up") targetAction.ApplyBindingOverride(1, new_Path);
        else if (action == "Down") targetAction.ApplyBindingOverride(3, new_Path);
        else if (action == "Left") targetAction.ApplyBindingOverride(2, new_Path);
        else if (action == "Right") targetAction.ApplyBindingOverride(4, new_Path);
        else targetAction.ApplyBindingOverride(0, new_Path);
        canvas.GetChild(2).GetComponent<ShowControlls>().UpdateControls();
        bindingEndTime = -10;
    }
    public void Quit()=> Application.Quit();//shortened for convenience, however, this isalso because I do not yet have saving between sessions working
}