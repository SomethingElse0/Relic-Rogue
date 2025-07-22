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
    public void UpdateControls()
    {
        int i = 0;
        Transform keybind;
        Transform text;
        string keyName;
        while (i+2 < transform.childCount)
        {
            keybind=transform.GetChild(i+1);
            try { keyName= actions.FindActionMap("Movement").FindAction(keybind.name).bindings[0].effectivePath.ToUpper(); }
            catch
            {
                keyName=actions.FindActionMap("Movement").FindAction("movement").bindings[i+1].effectivePath.ToUpper();
            }
            keyName=keyName.Substring(keyName.LastIndexOf('/') + 1);
            keybind.GetComponentInChildren<TMPro.TextMeshProUGUI>(true).SetText(keybind.name + " : "+keyName);
            i++;
            }
    }
}
