using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class InputSystemController : MonoBehaviour
{
    // Start is called before the first frame update
    bool paused;
    void HandleJump(InputAction.CallbackContext context)
    {
        print(context.phase);
        if (context.performed) print("Jumped");
        else if (context.started) print("Jumping");
        else if (context.canceled) print("FakeJump");
    }

    // Update is called once per frame

}
