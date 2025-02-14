using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    string upKey = "w";
    string downKey = "s";
    string leftKey = "a";
    string rightKey = "d";
    string dashKey = "Space";
    float playerSpeed = 6;
    float dashCoolldown = 4.5f;
    float lastDashTime = -10;
    Vector3 velocity;
    Vector3 savedVelocity=new Vector3 (1,0,0);
    Vector3 dashDirection;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upKey)) velocity.y++;
        else if (Input.GetKeyUp(upKey)) velocity.y--;
        if (Input.GetKeyDown(downKey)) velocity.y--;
        else if (Input.GetKeyUp(downKey)) velocity.y++;
        if (Input.GetKeyDown(leftKey)) velocity.x--;
        else if (Input.GetKeyUp(leftKey)) velocity.x++;
        if (Input.GetKeyDown(rightKey)) velocity.x++;
        else if (Input.GetKeyUp(rightKey)) velocity.x--;
        if (Input.GetKeyDown(dashKey)) OnDash();
        if (Time.fixedTime > lastDashTime + 0.1f)
        {
            velocity = dashDirection;
        }
        else
        {
            if (velocity.magnitude > 1) rb.velocity = velocity * playerSpeed / velocity.magnitude;
            else rb.velocity = velocity * playerSpeed;
        }
        if (velocity.magnitude > 0 && velocity / velocity.magnitude != savedVelocity) savedVelocity = velocity / velocity.magnitude;
    }
    void OnDash()
    {
        if (lastDashTime + dashCoolldown < Time.fixedTime)
        {
            lastDashTime = Time.fixedTime;
            if (velocity.magnitude > 0) dashDirection = velocity * 7 / velocity.magnitude;
            else dashDirection = savedVelocity * 7 / velocity.magnitude;
        }
    }
}
