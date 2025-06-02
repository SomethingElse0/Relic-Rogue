using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash_Destination : MonoBehaviour
{
    // Start is called before the first frame update
    InputAction movement;
    Rigidbody rb;
    float magnitude;
    Vector2 newNormalDirection, normalDirection;
    Vector3 direction;
    bool hitSomething = false;
    float playerSpeed = 2.5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponentInParent<PlayerMovement>().actions.FindActionMap("Movement").FindAction("movement");
        
        normalDirection = new Vector2(1,0);
    }
    private void Update()
    {
        direction = transform.parent.GetComponent<Rigidbody>().velocity;
        if (direction.magnitude != 0) transform.position = transform.parent.position + new Vector3(direction.normalized.x * magnitude, direction.normalized.y * magnitude, 0);
        if (hitSomething == false && magnitude +1< playerSpeed) magnitude ++;
        else if (hitSomething == false) magnitude = playerSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        
    }
    private void OnCollisionStay(Collision collision)
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
        transform.position = transform.parent.position;
    }
}
//90