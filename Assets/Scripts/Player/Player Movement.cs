using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionAsset actions;
    InputAction dash;
    InputAction attack;
    InputAction movement;
    InputAction move;
    InputAction interact;
    GameObject weapon;
    GameObject dashDestination;
    Ray ray;
    float playerSpeed = 5f;
    float dashCoolldown = 1.5f;
    float lastDashTime = -10;
    float hp = 20;
    Vector3 velocity=new Vector3 (0,0,0);
    Vector3 savedVelocity=new Vector3 (0,1,0);
    Vector3 dashDirection;
    Rigidbody rb;
    GameObject interactableObject;
    
    void Start()
    {
        dashDestination = transform.GetChild(0).gameObject;
        dash = actions.FindActionMap("Movement").FindAction("dash");
        actions.FindActionMap("Movement").FindAction("dash").performed += OnDash;
        interact = actions.FindActionMap("Movement").FindAction("interact");
        actions.FindActionMap("Movement").FindAction("interact").performed += OnInteract;
        movement = actions.FindActionMap("Movement").FindAction("movement");
        attack = actions.FindActionMap("Movement").FindAction("attack");
        actions.FindActionMap("Movement").FindAction("attack").performed += OnAttack;
        rb = GetComponent<Rigidbody>();
        ray.origin = transform.position;
        ray.direction = dashDestination.transform.localPosition;
    }
    private void Awake()
    {
        
    }
    private void OnEnable() => actions.Enable();
    private void OnDisable() => actions.Disable();
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet") hp -= collision.gameObject.GetComponent<Bullet>().damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        interactableObject = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        if (interactableObject == other.gameObject) interactableObject = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (hp < 0) playerSpeed = 0;
        else
        {
            Vector2 movementValue = playerSpeed * movement.ReadValue<Vector2>();
            rb.velocity = new Vector3(movementValue.x, movementValue.y, 0);
            Debug.Log("hi" + movement.ReadValue<Vector2>().x);

            if (velocity.magnitude != 0 && velocity / velocity.magnitude != savedVelocity) savedVelocity = velocity / velocity.magnitude;
        }
    }
    void OnInteract(InputAction.CallbackContext context)
    {
        if (interactableObject!=null)interactableObject.SendMessage("Interact");
    }
    void OnDash(InputAction.CallbackContext context)
    {
        if (lastDashTime + dashCoolldown < Time.fixedTime)
        {
            lastDashTime = Time.fixedTime;
            if (velocity.magnitude > 0) dashDirection = velocity * playerSpeed*3 / velocity.magnitude;
            else dashDirection = savedVelocity * playerSpeed*3;

            Vector3 correction = new Vector3(-1, 0);
            dashDestination.GetComponent<Dash_Destination>().Dash();
            OnDisable();
            transform.position = dashDestination.transform.position;
            OnEnable();
            dashDestination.GetComponent<Dash_Destination>().PositionReset();
        }
    }
    void OnMove(InputAction.CallbackContext context)
    {
        velocity = new Vector3(movement.ReadValue<Vector2>().x, movement.ReadValue<Vector2>().y,0);
    }
    void OnAttack(InputAction.CallbackContext context)
    {
        /*if (lastDashTime + 0.1f > Time.fixedTime) weapon.SendMessage("dashAttack");
        else weapon.SendMessage("attack");*/
        print("hi");
    }
    void OnPause()
    {
        if (actions.enabled == true) OnDisable();
        else OnEnable();
    }
}
