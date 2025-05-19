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
    Vector2 direction;
    bool hitSomething = false;
    float playerSpeed = 2.5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponentInParent<PlayerMovement>().actions.FindActionMap("Movement").FindAction("movement");
        direction = movement.ReadValue<Vector2>();
        normalDirection = new Vector2(1,0);
    }
    private void Update()
    {
        if (direction.magnitude > 0) normalDirection = newNormalDirection = direction / direction.magnitude;
        else newNormalDirection = new Vector2(0, 0);
        transform.position = transform.parent.position+new Vector3(normalDirection.x, normalDirection.y)*magnitude;
        print((transform.position - transform.parent.position));
        if (hitSomething == false && magnitude < playerSpeed - 1) magnitude += 1;
        else if (hitSomething == false) magnitude = playerSpeed;
        else magnitude -= 0.1f;
    }
    public void Dash()
    {
        if (direction.magnitude>0) transform.position = new Vector3(normalDirection.x, normalDirection.y, 0)*magnitude + transform.position;
        print((transform.position - transform.parent.position));
    }
    
    private void OnCollisionStay(Collision collision)
    {
        hitSomething = true;
        Vector3 localColisionDistance = collision.GetContact(0).point-transform.parent.position;
        transform.position = transform.parent.position+((transform.position - transform.parent.position) * localColisionDistance.magnitude / (transform.position - transform.parent.position).magnitude);
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