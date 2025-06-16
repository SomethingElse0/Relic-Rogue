using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash_Destination : MonoBehaviour
{
    // Start is called before the first frame update
    InputAction movement;
    float magnitude;
    Vector2 newNormalDirection, normalDirection;
    Vector3 direction;
    Transform player;
    bool hitSomething = false;
    float playerSpeed = 2.5f;
    Ray newRay;
    RaycastHit rayHit;
    LayerMask mask;
    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>().actions.FindActionMap("Movement").FindAction("movement");
        mask = transform.GetComponent<Collider>().includeLayers;
        normalDirection = new Vector2(1,0);
    }
    private void Update()
    {
        player = transform.parent;
        direction = movement.ReadValue<Vector2>().normalized;
        if (direction.magnitude > 0)
        {
            newRay = new Ray(player.position + ((transform.position - player.position).normalized * 0.5f), direction);
            if (Physics.Raycast(newRay, out rayHit, 5, mask))
            {
                if (rayHit.distance < 1) transform.position = player.position;
                else transform.position = rayHit.point - 0.5f * direction.normalized;
            }
            else transform.position = player.position + 4 * direction;
        }
    }
    void OnDash()
    {
        
        player.SendMessage("OnDisable", SendMessageOptions.DontRequireReceiver);
        player.position = transform.position;
        player.SendMessage("OnEnable", SendMessageOptions.DontRequireReceiver);
        PositionReset();
    }
    private void OnCollisionEnter(Collision collision)
    {
        hitSomething = true;
        
    }

    private void OnCollisionExit(Collision collision)
    {
        hitSomething = false;
    }
    // Update is called once per frame
    public void PositionReset()
    {
        transform.position = player.position;
    }
}
//90