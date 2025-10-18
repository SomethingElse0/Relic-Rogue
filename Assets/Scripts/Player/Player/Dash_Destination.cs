using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash_Destination : MonoBehaviour
{
    // Start is called before the first frame update
    InputAction movement;
    float magnitude;
    Vector3 direction;
    Transform player;

    Ray newRay;
    RaycastHit rayHit;
    LayerMask mask;
    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>().actions.FindActionMap("Movement").FindAction("movement");
        mask = transform.GetComponent<Collider>().includeLayers;//defining which layers are included when chacking for collisions
    }
    private void Update()
    {
        player = transform.parent;
        direction = movement.ReadValue<Vector2>().normalized;
        if (direction.magnitude > 0)
        {
            newRay = new Ray(player.position + ((transform.position - player.position).normalized * 0.5f), direction);//this is to check for any obstacles, and if so, how far away the closest colision is 
            if (Physics.Raycast(newRay, out rayHit, 5, mask))
            {
                if (rayHit.distance < 1) transform.position = player.position;
                else transform.position = rayHit.point - 0.5f * direction.normalized;
            }
            else transform.position = player.position + 4.5f * direction;
        }
    }
    public void OnDash()
    {

        player.GetComponent<PlayerMovement>().OnDisable();//temporarily disabling the player so that they are moved correctly
        player.position = transform.position;
        player.GetComponent<PlayerMovement>().OnEnable();
        PositionReset();
    }
    // Update is called once per frame
    public void PositionReset()=>transform.position = player.position;
}
//90