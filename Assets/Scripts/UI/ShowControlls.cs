using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowControlls : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionAsset actions;
    void Awake()
    {
        UpdateControls();
    }

    // Update is called once per frame
    public void InputKeyBind(GameObject keybind)
    {
        keybind.GetComponentInChildren<TMPro.TextMeshProUGUI>(true).SetText(keybind.name + " : ..." );
    }
    public void UpdateControls()
    {
        int i = 0;
        Transform keybind;
        string keyName;
        while (i+2 < transform.childCount)
        {
            keybind=transform.GetChild(i+1);
            try {
                keyName = InputControlPath.ToHumanReadableString(
                  actions.FindActionMap("Movement").FindAction(keybind.name).bindings[0].effectivePath,
                  InputControlPath.HumanReadableStringOptions.OmitDevice);
            }
            catch
            {
                keyName=InputControlPath.ToHumanReadableString(
                    actions.FindActionMap("Movement").FindAction("movement").bindings[i+1].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice);
            }
            keyName=keyName.Substring(keyName.LastIndexOf('/') + 1);
            keybind.GetComponentInChildren<TMPro.TextMeshProUGUI>(true).SetText(keybind.name + " : "+keyName);
            i++;
            }
    }
}
