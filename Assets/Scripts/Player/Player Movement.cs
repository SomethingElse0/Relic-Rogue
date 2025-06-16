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
    InputAction changeWeapon;
    GameObject weapon;
    GameObject dashDestination;
    public List<ScriptableObject> weapons = new List<ScriptableObject>();
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
    int coin;
    int fuel;
    int scrap;
    int rations;
    bool levelKey=false;
    
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
        changeWeapon = actions.FindActionMap("Movement").FindAction("change");
        actions.FindActionMap("Movement").FindAction("attack").performed += ChangeWeapon;
        rb = GetComponent<Rigidbody>();
        ray.origin = transform.position;
        ray.direction = dashDestination.transform.localPosition;
        weapon=transform.GetChild(transform.childCount - 1).gameObject;
        weapon.SendMessage("ChangeWeapon", weapons[0], SendMessageOptions.DontRequireReceiver);
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
        if (interactableObject.tag == "ammo") weapon.SendMessage("PickupBullet", SendMessageOptions.DontRequireReceiver);
    }
    private void OnTriggerExit(Collider other)
    {
        if (interactableObject == other.gameObject) interactableObject = null;
    }
    void Update()
    {
        if (hp < 0)
        {
            playerSpeed = 0;
            actions.Disable();
        }
        else
        {
            Vector2 movementValue = playerSpeed * movement.ReadValue<Vector2>();
            rb.velocity = new Vector3(movementValue.x, movementValue.y, 0);

            if (velocity.magnitude != 0 && velocity / velocity.magnitude != savedVelocity) savedVelocity = velocity / velocity.magnitude;
        }   
    }
    void OnInteract(InputAction.CallbackContext context)
    {
        if (interactableObject!=null)interactableObject.SendMessage("Interact",SendMessageOptions.DontRequireReceiver);
    }
    void OnDash(InputAction.CallbackContext context)
    {
        if (lastDashTime + dashCoolldown < Time.fixedTime)
        {
            lastDashTime = Time.fixedTime;
            if (velocity.magnitude > 0) dashDirection = velocity * playerSpeed*3 / velocity.magnitude;
            else dashDirection = savedVelocity * playerSpeed*3;
            dashDestination.SendMessage("OnDash");
            Vector3 correction = new Vector3(-1, 0);
            
        }
    }
    void OnMove(InputAction.CallbackContext context)
    {
        velocity = new Vector3(movement.ReadValue<Vector2>().x, movement.ReadValue<Vector2>().y,0);
    }
    void OnAttack(InputAction.CallbackContext context)
    {
        weapon.SendMessage("Attack", SendMessageOptions.DontRequireReceiver);
    }
    void OnPause()
    {
        if (actions.enabled == true) OnDisable();
        else OnEnable();
    }
    void Pickup(string item)
    {
        if (item == "coin") coin++;
        else if (item == "scrap") scrap++;
        else if (item == "fuel") fuel++;
        else if (item == "rations") rations++;
        else if (item == "levelKey") levelKey=true;
    }
    void OpenDoor(GameObject door)
    {
        if (levelKey == true) door.SendMessage("hasKey", SendMessageOptions.DontRequireReceiver);
        levelKey = false;
    }
    void ChangeWeapon(InputAction.CallbackContext callbackContext)
    {
        weapons.Add(weapons[0]);
        weapon.SendMessage("ChangeWeapon", weapons[1], SendMessageOptions.DontRequireReceiver);
        weapons.RemoveAt(0);
    }
}
